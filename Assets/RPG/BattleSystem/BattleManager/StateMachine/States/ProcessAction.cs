using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterSystem;
using RPG.SkillSystem;

namespace RPG.BattleSystem
{
    public class ProcessAction : State<BattleController>
    {
        #region Singleton
        private static ProcessAction _instance;

        private ProcessAction ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static ProcessAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    new ProcessAction ();
                }

                return _instance;
            }
        }
        #endregion

        Character selectedCharacter;
        Character selectedTarget;
        Skill selectedSkill;

        public override void EnterState (BattleController owner)
        {
            if (owner.isPlayerTurn)
            {
                selectedCharacter = owner.Ally[owner.fastestCharacterIndex];
                selectedTarget = owner.Enemy[owner.selectedTargetIndex];
                selectedSkill = selectedCharacter.Skill[owner.selectedSkillIndex];

                for (int i = 0; i < selectedSkill.Effect.Count; i++)
                {
                    if (selectedSkill.Effect[i].IsConditionMet (selectedCharacter, selectedTarget))
                    {
                        Debug.Log ("Apply effect: " + selectedSkill.Effect[i].EffectType.ToString ());
                        selectedSkill.Effect[i].ApplyEffect (selectedCharacter, owner.Ally, selectedTarget, owner.Enemy);
                    }                        
                }


                if (selectedSkill.TargetType == TargetTypes.ALL_ENEMIES)
                {
                    for (int i = 0; i < owner.Enemy.Count; i++)
                    {
                        if (!owner.Enemy[i].IsDead)
                            CalculateDamage (selectedCharacter, selectedSkill, owner.Enemy[i]);
                    }
                }
                else
                {
                    //ProcessSkillPassiveEffect (owner.Ally, owner.Enemy);
                    CalculateDamage (selectedCharacter, selectedSkill, selectedTarget);
                }

                owner.Ally[owner.fastestCharacterIndex].CurActionBar = 0;
            }
            else
            {
                //CalculateDamage(Enemy, Ally);
                owner.Enemy[owner.fastestCharacterIndex].CurActionBar = 0;
            }

            foreach (CharacterUI ui in owner.battleUIManager.AllyUI)
            {
                ui.UpdateCharacterHPAndMP ();
            }

            foreach (CharacterUI ui in owner.battleUIManager.EnemyUI)
            {
                ui.UpdateCharacterHPAndMP ();
            }


            owner.stateMachine.ChangeState (DetermineNextState.Instance);

        }

        public override void ExecuteState (BattleController owner)
        {

        }

        public override void ExitState (BattleController owner)
        {

        }
        

        void CalculateDamage (Character selectedCharacter, Skill selectedSkill, Character selectedTarget)
        {
            //Debug.Log ("Attack " + Attacker[fastestCharacterIndex].CurAttack);
            //Debug.Log ("Defense " + Defender[selectedTargetIndex].CurDefense);
            //Debug.Log ("Base Power " + Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].BasePower);

            int dmg = (int)((float)selectedCharacter.CurAttack / selectedTarget.CurDefense * selectedSkill.BasePower);
            
            if (IsCriticalHit (selectedCharacter.CurCriticalRate))
            {
                dmg += (int)(dmg * (selectedCharacter.CurCiticalDamage / 100f));
                BattleController.instance.OnCharacterHealthChangedCallback.Invoke (dmg, selectedTarget.Prefab.transform.position, true, true);

                // TODO: should or should not use event???
                //BattleController.instance.battleUIManager.ShowPopupText (dmg, selectedTarget.Prefab.transform.position, true, true);
            }
            else
            {                
                BattleController.instance.OnCharacterHealthChangedCallback.Invoke (dmg, selectedTarget.Prefab.transform.position, true, false);
            }

            selectedTarget.CurHP -= dmg;

            if (selectedTarget.CurHP <= 0)
                selectedTarget.SetCharacterStatusToDead ();            
        }


        /// <summary>
        /// Determine it is critical hit or not
        /// </summary>
        /// <param name="criticalRate"></param>
        /// <returns></returns>
        bool IsCriticalHit (int criticalRate)
        {
            int randomNumber = UnityEngine.Random.Range (0, 100);
            //Debug.Log ("Random number: " + randomNumber);

            if (randomNumber < criticalRate)
                return true;
            else
                return false;
        }        
    }
}
