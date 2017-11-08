using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.BattleSystem
{
    public class TurnStart : State<BattleController>
    {
        #region Singleton
        private static TurnStart _instance;

        private TurnStart ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static TurnStart Instance
        {
            get
            {
                if (_instance == null)
                {
                    new TurnStart ();
                }

                return _instance;
            }
        }
        #endregion

        public override void EnterState (BattleController owner)
        {
            owner.turn++;
            Debug.Log ("Turn " + owner.turn + ":");

            owner.stateMachine.ChangeState (DetermineWhichCharacterTurn.Instance);
        }

        public override void ExecuteState (BattleController owner)
        {
            
        }

        public override void ExitState (BattleController owner)
        {

        }
    }
}
