using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.BattleSystem
{
    public class EnemyChoice : State<BattleController>
    {
        #region Singleton
        private static EnemyChoice _instance;

        private EnemyChoice ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static EnemyChoice Instance
        {
            get
            {
                if (_instance == null)
                {
                    new EnemyChoice ();
                }

                return _instance;
            }
        }
        #endregion

        public override void EnterState (BattleController owner)
        {

        }

        public override void ExecuteState (BattleController owner)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // need to implantment AI here and change state to ProcessAction
                owner.stateMachine.ChangeState (ProcessAction.Instance);
            }

            //owner.StartCoroutine (WaitForSecound (5));
        }
        
        public override void ExitState (BattleController owner)
        {

        }

        IEnumerator WaitForSecound (int sec)
        {
            yield return new WaitForSeconds (sec);
        }
    }
}
