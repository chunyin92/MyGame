using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.BattleSystem
{
    public class Win : State<BattleController>
    {
        #region Singleton
        private static Win _instance;

        private Win ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static Win Instance
        {
            get
            {
                if (_instance == null)
                {
                    new Win ();
                }

                return _instance;
            }
        }
        #endregion

        public override void EnterState (BattleController owner)
        {
            owner.ResetVariables ();
            

            GameManager.instance.LeaveBattle ();
        }

        public override void ExecuteState (BattleController owner)
        {
            
        }

        public override void ExitState (BattleController owner)
        {
            
        }
    }
}
