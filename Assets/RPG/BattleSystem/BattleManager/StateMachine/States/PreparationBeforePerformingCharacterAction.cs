using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SkillSystem;

namespace RPG.BattleSystem
{
    public class PreparationBeforePerformingCharacterAction : State<BattleController>
    {
        #region Singleton
        private static PreparationBeforePerformingCharacterAction _instance;

        private PreparationBeforePerformingCharacterAction ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static PreparationBeforePerformingCharacterAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    new PreparationBeforePerformingCharacterAction ();
                }

                return _instance;
            }
        }
        #endregion

        public override void EnterState (BattleController owner)
        {

            if (owner.isPlayerTurn)
            {
                // one turn passed when action bar reached 1, have to refresh all counter, i.e. buff duration and cool down counter  
                owner.Ally[owner.fastestCharacterIndex].MinusAllSkillCoolDownCounterByOne ();
                owner.Ally[owner.fastestCharacterIndex].MinusAllStatusDurationByOne ();
                owner.stateMachine.ChangeState (PlayerChoice.Instance);
            }
            else
            {
                owner.stateMachine.ChangeState (EnemyChoice.Instance);
            }
                
        }

        public override void ExecuteState (BattleController owner)
        {

        }

        public override void ExitState (BattleController owner)
        {

        }
    }
}
