using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterSystem;

namespace RPG.BattleSystem
{
    public class DetermineNextState : State<BattleController>
    {
        #region Singleton
        private static DetermineNextState _instance;

        private DetermineNextState ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static DetermineNextState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new DetermineNextState ();
                }

                return _instance;
            }
        }
        #endregion

        public override void EnterState (BattleController owner)
        {
            if (AreAllCharacterDead (owner.Enemy))
            {
                owner.stateMachine.ChangeState (Win.Instance);
            }
            else if (AreAllCharacterDead (owner.Ally))
            {
                Debug.Log ("Entering Lose State");
            }
            else
            {
                owner.fastestCharacterIndex = -1;
                owner.selectedSkillIndex = -1;
                owner.selectedTargetIndex = -1;

                owner.stateMachine.ChangeState (DetermineWhichCharacterTurn.Instance);
            }
        }

        public override void ExecuteState (BattleController owner)
        {
            
        }

        public override void ExitState (BattleController owner)
        {

        }

        bool AreAllCharacterDead (List<Character> characters)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if (!characters[i].IsDead)
                    return false;
            }

            return true;
        }
    }
}
