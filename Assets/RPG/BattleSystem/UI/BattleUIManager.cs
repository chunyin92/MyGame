using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.BattleSystem;
using RPG.CharacterSystem;
using TMPro;

namespace RPG.BattleSystem
{
    public class BattleUIManager : MonoBehaviour
    {        
        public Slider[] AllyActionBars;
        public Slider[] EnemyActionBars;
        public Button[] SkillButtons;
        public CharacterUI[] AllyUI;
        public CharacterUI[] EnemyUI;
        public Transform CurCanvas;
        public GameObject PopUpText;

        BattleController _battleContorller;

        void Awake ()
        {
            if (AllyActionBars == null)
                Debug.LogWarning ("Please assign!");

            if (EnemyActionBars == null)
                Debug.LogWarning ("Please assign!");

            if (SkillButtons == null)
                Debug.LogWarning ("Please assign!");

            if (AllyUI == null)
                Debug.LogWarning ("Please assign!");

            if (EnemyUI == null)
                Debug.LogWarning ("Please assign!");
        }

        void Start ()
        {
            if (BattleController.instance == null)
                Debug.LogWarning ("There is no battle controller!");

            _battleContorller = BattleController.instance;
            _battleContorller.OnCharacterActionBarChangedCallback += UpdateActionBar;
            _battleContorller.OnCharacterHealthChangedCallback += ShowPopupText;
        }

        void Update ()
        {

        }

        void UpdateActionBar ()
        {
            if (_battleContorller.Ally == null)
                Debug.Log ("Ally == null");
            else
            {
                for (int i = 0; i < _battleContorller.Ally.Count; i++)
                {
                    if (_battleContorller.Ally[i].IsDead)
                        AllyActionBars[i].value = 0;
                    else
                        AllyActionBars[i].value = _battleContorller.Ally[i].CurActionBar;
                }
            }


            if (_battleContorller.Enemy == null)
                Debug.Log ("Enemy == null");
            else
            {
                for (int i = 0; i < _battleContorller.Enemy.Count; i++)
                {
                    if (_battleContorller.Ally[i].IsDead)
                        EnemyActionBars[i].value = 0;
                    else
                        EnemyActionBars[i].value = _battleContorller.Enemy[i].CurActionBar;
                }
                    
            }

        }

        public void DisplayCharacterSkill (Character character)
        {
            //Debug.Log (SkillButtons.Length);
            //Debug.Log (character.Skill.Count);
            
            // TODO: need to spawn buttons match with skills number
            for (int i = 0; i < character.Skill.Count; i++)
            {
                SkillButtons[i].gameObject.SetActive (true);

                if (character.Skill[i].IsOnCooldown)
                {
                    SkillButtons[i].interactable = false;
                    SkillButtons[i].GetComponentInChildren<TextMeshProUGUI> ().text = character.Skill[i].Name + " |on CD: " + character.Skill[i].CooldownCounter;
                }
                else
                {
                    SkillButtons[i].GetComponentInChildren<TextMeshProUGUI> ().text = character.Skill[i].Name;
                }
            }
        }

        public void HideCharacterSkill ()
        {
            for (int i = 0; i < SkillButtons.Length; i++)
            {
                SkillButtons[i].interactable = true;
                SkillButtons[i].gameObject.SetActive (false);
            }
        }

        public void ShowPopupText (int damage, Vector3 position, bool isDamage, bool isCritical)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint (position);
            GameObject go = TrashMan.spawn (PopUpText);
            go.transform.SetParent (CurCanvas.transform, false);

            string text;

            if (isCritical)
                text = "Crit " + damage.ToString ();
            else
                text = damage.ToString ();

            go.GetComponent<PopUpText> ().DisplayText (text, isDamage);
            go.transform.position = pos;
        }

        void OnDestroy ()
        {
            _battleContorller.OnCharacterActionBarChangedCallback -= UpdateActionBar;
            _battleContorller.OnCharacterHealthChangedCallback -= ShowPopupText;
        }
    }
}
