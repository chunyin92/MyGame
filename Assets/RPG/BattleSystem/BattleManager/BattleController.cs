using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CharacterSystem;
using System;

namespace RPG.BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;
        public BattleUIManager battleUIManager;

        public StateMachine<BattleController> stateMachine { get; set; }

        //public GameObject[] CharacterUI;
        //public Button[] SkillButtons;
        //public Slider[] TimeLine;
        //public List<Transform> AllyPosition;
        //public List<Transform> EnemyPosition;
        public List<Vector3> AllyPos;
        public List<Vector3> EnemyPos;

        public List<Character> Ally = new List<Character> ();
        public List<Character> Enemy = new List<Character> ();

        public Action OnCharacterActionBarChangedCallback;
        public Action<int, Vector3, bool, bool> OnCharacterHealthChangedCallback;

        public int turn = 0;
        //public float shortestTime;
        public int fastestCharacterIndex = -1;
        public int selectedSkillIndex = -1;
        public int selectedTargetIndex = -1;
        public bool isPlayerTurn;

        public GameObject popupText;
        public Canvas curCanvas;

        void Awake ()
        {
            #region Singleton

            if (instance != null)
            {
                Debug.LogWarning ("More then one BattlerContoller");
                Destroy (gameObject);
                return;
            }
            else
                instance = this;

            #endregion

            DontDestroyOnLoad (gameObject);

            if (!battleUIManager)
                Debug.LogError ("Please assign!");

            stateMachine = new StateMachine<BattleController> (this);
        }
        
        private void Start ()
        {
            //stateMachine = new StateMachine<BattleController> (this);
            //stateMachine.ChangeState (BattleInit.Instance);
        }

        private void Update ()
        {
            stateMachine.Update ();
        }

        // for character position in battle ground
        void OnDrawGizmosSelected ()
        {
            Gizmos.color = Color.green;
            foreach (Vector3 pos in AllyPos)
            {
                Gizmos.DrawSphere (pos, 0.5f);
            }
            Gizmos.color = Color.red;
            foreach (Vector3 pos in EnemyPos)
            {
                Gizmos.DrawSphere (pos, 0.5f);
            }
            Gizmos.color = Color.white;
        }

        public void ResetVariables ()
        {
            Ally.Clear ();
            Enemy.Clear ();
            turn = 0;
            fastestCharacterIndex = -1;
            selectedSkillIndex = -1;
            selectedTargetIndex = -1;
    }
    }
}
