using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CharacterSystem;
using RPG.SkillSystem;
using TMPro;

namespace RPG.BattleSystem
{
    public class PlayerChoice : State<BattleController>
    {
        #region Singleton
        private static PlayerChoice _instance;

        private PlayerChoice ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static PlayerChoice Instance
        {
            get
            {
                if (_instance == null)
                {
                    new PlayerChoice ();
                }

                return _instance;
            }
        }
        #endregion
        
        public override void EnterState (BattleController owner)
        {

            owner.battleUIManager.DisplayCharacterSkill (owner.Ally[owner.fastestCharacterIndex]);
        }

        public override void ExecuteState (BattleController owner)
        {
            if (owner.selectedSkillIndex != -1)
            {
                SelectTarget (owner.Ally[owner.fastestCharacterIndex], owner.Enemy, owner);
            }
        }

        public override void ExitState (BattleController owner)
        {
            owner.battleUIManager.HideCharacterSkill ();
        }
        
        void SelectTarget (Character curCharacter, List<Character> Enemy, BattleController owner)
        {
            if (Input.GetMouseButtonDown (0))
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                
                if (Physics.Raycast (ray, out hit))
                {
                    for (int i = 0; i < Enemy.Count; i++)
                    {
                        if (hit.collider.gameObject == Enemy[i].Prefab && !Enemy[i].IsDead)
                        {
                            owner.selectedTargetIndex = i;
                            curCharacter.Skill[owner.selectedSkillIndex].SetCoolDownCounterToMax ();
                            
                            //curCharacter.IsMoved = true;

                            Debug.Log (curCharacter.Name + " Attacked " + Enemy[owner.selectedTargetIndex].Name + " with skill: " + curCharacter.Skill[owner.selectedSkillIndex].Name);
                            owner.stateMachine.ChangeState (ProcessAction.Instance);
                        }
                    }                    
                }
            }
        }



    }
}
