using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterSystem;

namespace RPG.BattleSystem
{
    public class BattleInit : State<BattleController>
    {
        #region Singleton
        private static BattleInit _instance;

        private BattleInit ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static BattleInit Instance
        {
            get
            {
                if (_instance == null)
                {
                    new BattleInit ();
                }

                return _instance;
            }
        }
        #endregion

        CharacterDatabase characterdb;

        public override void EnterState (BattleController owner)
        {
            LoadCharacterDatabase ();
            GetAllyInfoAndSetup (owner);
            GetEnemyInfoAndSetup (owner);
            SetupCharacterInfoUI (owner);
            owner.stateMachine.ChangeState (TurnStart.Instance);
        }

        public override void ExecuteState (BattleController owner)
        {
            //Debug.Log ("Executing BattleInit State");
        }

        public override void ExitState (BattleController owner)
        {

        }

        void LoadCharacterDatabase ()
        {
            characterdb = Resources.Load ("Database/CharacterDatabase") as CharacterDatabase;

            if (characterdb == null)
                Debug.LogWarning ("Character Database not loaded");
        }

        void GetAllyInfoAndSetup (BattleController owner)
        {
            // TODO: need to rework so that system can get player characters from party
            for (int i = 0; i < 2; i++)
                owner.Ally.Add (new Character (characterdb.Get (i), 5));

            for (int i = 0; i < owner.Ally.Count; i++)
            {
                owner.Ally[i].Prefab = UnityEngine.Object.Instantiate (owner.Ally[i].Prefab, owner.AllyPos[i], Quaternion.identity) as GameObject;
                owner.Ally[i].Prefab.transform.SetParent (owner.transform);
            }
        }

        void GetEnemyInfoAndSetup (BattleController owner)
        {
            // TODO: need to rework so that system can get enemy from encounter
            //for (int i = 2; i < 4; i++)
            //    owner.Enemy.Add (new Character (characterdb.Get (i), 5));

            for (int i = 0; i < owner.Enemy.Count; i++)
            {
                owner.Enemy[i].Prefab = UnityEngine.Object.Instantiate (owner.Enemy[i].Prefab, owner.EnemyPos[i], Quaternion.identity) as GameObject;
                owner.Enemy[i].Prefab.transform.SetParent (owner.transform);
            }
        }

        void SetupCharacterInfoUI (BattleController owner)
        {
            for (int i = 0; i < owner.Ally.Count; i++)
                owner.battleUIManager.AllyUI[i].Init (owner.Ally[i]);

            for (int i = 0; i < owner.Enemy.Count; i++)
                owner.battleUIManager.EnemyUI[i].Init (owner.Enemy[i]);
        }        
    }
}
