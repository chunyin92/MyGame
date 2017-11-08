using System;
using UnityEngine;

namespace RPG.BattleSystem
{
    public class FirstState : State<BattleController>
    {
        private static FirstState _instance;

        private FirstState ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static FirstState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new FirstState ();
                }

                return _instance;
            }
        }

        public override void EnterState (BattleController owner)
        {
            Debug.Log ("Entering First State");
        }

        public override void ExitState (BattleController owner)
        {
            Debug.Log ("Exiting First State");
        }

        public override void ExecuteState (BattleController owner)
        {
            //if (_owner.switchState)
            //{
            //    _owner.stateMachine.ChangeState (SecondState.Instance);
            //}
        }
    }
}