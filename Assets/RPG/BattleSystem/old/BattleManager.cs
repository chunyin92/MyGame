using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using RPG.CharacterSystem;
using TMPro;

namespace RPG.BattleSystem
{
    public partial class BattleManager : MonoBehaviour
    {
        public GameObject[] CharacterUI;
        public Button[] SkillButtons;
        public Slider[] TimeLine;


        public static BattleManager instance;

        public List<Transform> AllyPosition;
        public List<Transform> EnemyPosition;

        public List<Character> Ally = new List<Character> ();
        public List<Character> Enemy = new List<Character> ();
        


        //static List<Character> Ally = new List<Character>();
        //static List<Character> Enemy = new List<Character>();        

        bool displayAllyInfo = false;
        bool displayEnemyInfo = false;

        BattleState currentBattleState;

        enum BattleState
        {
            BATTLE_START,
            TURN_START,
            DETERMINE_WHICH_CHARACTER_TURN,
            PLAYER_CHOICE,
            ENEMY_CHOICE,
            PROCESS_ACTION,
            DETERMINE_NEXT_STATE,
            WIN,
            LOSE,
            BATTLE_END
        }

        void Awake ()
        {
            #region Singleton

            if (instance != null)
            {
                Debug.LogWarning ("More then one BattleManager");
                Destroy (gameObject);
                return;
            }
            else
                instance = this;

            #endregion

            DontDestroyOnLoad (gameObject);
        }

        void Start()
        {
            currentBattleState = BattleState.BATTLE_START;
            BattleInit ();



            for (int i = 0; i < CharacterUI.Length; i++)
            {
                if (i < Ally.Count)
                    CharacterUI[i].GetComponent<CharacterUI> ().Init (Ally[i]);
                else
                    CharacterUI[i].GetComponent<CharacterUI> ().Init (Enemy[i - Ally.Count]);
            }
            


        }

        void Update()
        {
            //Debug.Log(currentBattleState);

            switch (currentBattleState)
            {
                case (BattleState.BATTLE_START):
                    //BattleInit ();
                    break;
                case (BattleState.TURN_START):
                    TurnStart();
                    break;
                case (BattleState.DETERMINE_WHICH_CHARACTER_TURN):
                    DetermineWhichCharacterTurn();
                    break;
                case (BattleState.PLAYER_CHOICE):
                    PlayerChoice();
                    break;
                case (BattleState.ENEMY_CHOICE):
                    // AI
                    // since there is no turn anymore with new speed system
                    //Enemy[fastestCharacterIndex].IsMoved = true;
                    currentBattleState = BattleState.DETERMINE_NEXT_STATE;
                    break;
                case (BattleState.PROCESS_ACTION):
                    ProcessAction();
                    break;
                case (BattleState.DETERMINE_NEXT_STATE):
                    DetermineNextState();
                    break;
                case (BattleState.WIN):
                    Debug.Log("You WIN");
                    currentBattleState = BattleState.BATTLE_END;
                    break;
                case (BattleState.LOSE):
                    Debug.Log("You LOSE");
                    currentBattleState = BattleState.BATTLE_END;
                    break;
                case (BattleState.BATTLE_END):
                    //GameManager.currentGameState = GameManager.GameState.NORAML;
                    break;
            }
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Ally Info"))
            {
                displayAllyInfo = !displayAllyInfo;                
            }
            DisplayCharacterInfo(Ally, displayAllyInfo);

            if (GUILayout.Button("Enemy Info"))
            {
                displayEnemyInfo = !displayEnemyInfo;                
            }
            
            DisplayCharacterInfo(Enemy, displayEnemyInfo);

            if (GUILayout.Button("End Battle"))
            {
                currentBattleState = BattleState.BATTLE_END;
            }


            GUILayout.EndHorizontal();

            //if(currentBattleState==BattleState.PLAYER_CHOICE)
            //    ShowCharacterSkill();
        }

        void DisplayCharacterInfo(List<Character> character, bool displayInfo)
        {
            if (displayInfo)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUILayout.Label("Name:");
                GUILayout.Label("Level:");
                GUILayout.Label("Max HP:");
                GUILayout.Label("Cur HP:");
                GUILayout.Label("Max MP:");
                GUILayout.Label("Cur MP:");
                GUILayout.Label("Attack:");
                GUILayout.Label("Defense:");
                GUILayout.Label("Talent:");
                GUILayout.Label("Speed:");
                GUILayout.Label("CriticalRate:");
                GUILayout.Label("CriticalDamage:");
                GUILayout.EndVertical();

                for (int i = 0; i < character.Count; i++)
                {
                    GUILayout.Space(50);

                    GUILayout.BeginVertical();

                    GUILayout.Label("" + character[i].Name);
                    GUILayout.Label("" + character[i].Level);
                    GUILayout.Label("" + character[i].MaxHP);
                    GUILayout.Label("" + character[i].CurHP);
                    GUILayout.Label("" + character[i].MaxMP);
                    GUILayout.Label("" + character[i].CurMP);
                    GUILayout.Label("" + character[i].CurAttack);
                    GUILayout.Label("" + character[i].CurDefense);
                    GUILayout.Label("" + character[i].Talent);
                    GUILayout.Label("" + character[i].CurSpeed);
                    GUILayout.Label("" + character[i].CurCriticalRate);
                    GUILayout.Label("" + character[i].CurCiticalDamage);

                    GUILayout.EndVertical();

                    //for (int j = 0; j < character[i]._baseCharacter.Skill.Count; j++)
                    //{
                    //    GUILayout.Label(character[i]._baseCharacter.Skill[j].Name);
                    //    GUILayout.Label(character[i]._baseCharacter.Skill[j].BasePower.ToString());
                    //}
                }
                GUILayout.EndHorizontal();
            }
        }



        void BattleInit()
        {
            CharacterDatabase characterdb = Resources.Load("Database/CharacterDatabase") as CharacterDatabase;

            if (characterdb == null)
                Debug.LogWarning("Character Database not loaded");


            // TODO: need to rework so that system can get player characters from party, and also enemy from the encounter
            //Debug.Log(characterdb.Get(1).Name);
            for (int i = 0; i < 2; i++)
            {
                Ally.Add(new Character(characterdb.Get(i), 5));
            }

            for (int i = 2; i < 4; i++)
            {
                Enemy.Add(new Character(characterdb.Get(i), 5));                
            }

            for (int i = 0; i < Ally.Count; i++)
            {
                Ally[i].Prefab = Instantiate(Ally[i].Prefab, AllyPosition[i].position, Quaternion.identity) as GameObject;
                //Ally[i]._prefab.name = i.ToString();
                //Ally[i]._prefab.tag = "Ally";
                Ally[i].Prefab.transform.SetParent(AllyPosition[i]);
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                Enemy[i].Prefab = Instantiate(Enemy[i].Prefab, EnemyPosition[i].position, Quaternion.identity) as GameObject;
                //Enemy[i]._prefab.name = i.ToString();
                //Enemy[i]._prefab.tag = "Enemy";
                Enemy[i].Prefab.transform.SetParent(EnemyPosition[i]);
            }

            currentBattleState = BattleState.TURN_START;
        }

        
    }
}
