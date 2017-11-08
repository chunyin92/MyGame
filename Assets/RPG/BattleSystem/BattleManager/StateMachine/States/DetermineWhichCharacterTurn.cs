using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterSystem;
using UnityEngine.UI;

namespace RPG.BattleSystem
{
    public class DetermineWhichCharacterTurn : State<BattleController>
    {
        #region Singleton
        private static DetermineWhichCharacterTurn _instance;

        private DetermineWhichCharacterTurn ()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static DetermineWhichCharacterTurn Instance
        {
            get
            {
                if (_instance == null)
                {
                    new DetermineWhichCharacterTurn ();
                }

                return _instance;
            }
        }
        #endregion

        float duration;

        public override void EnterState (BattleController owner)
        {
            duration = GetTimeNeededForFastestCharacterToReachMaxActionBar (owner.Ally, owner.Enemy);
            owner.StartCoroutine (AnimateSpeed (owner.Ally, owner.Enemy, owner));
        }

        public override void ExecuteState (BattleController owner)
        {
            CheckWhichCharacterActionBarReachMax (owner.Ally, owner.Enemy, owner);
        }

        public override void ExitState (BattleController owner)
        {

        }        

        float GetTimeNeededForFastestCharacterToReachMaxActionBar (List<Character> Ally, List<Character> Enemy)
        {
            // get the time need for the fastest character to max action bar so that we can animate the action bar of all characters
            // if the character is dead, simply ignore it and check the next character

            float shortestTime = 100;
            float timeNeededtoReachMaxActionBar;

            for (int i = 0; i < Ally.Count; i++)
            {
                if (Ally[i].IsDead)
                    continue;

                timeNeededtoReachMaxActionBar = (1f - Ally[i].CurActionBar) / (Ally[i].CurSpeed / 100f);

                if (timeNeededtoReachMaxActionBar < shortestTime)
                    shortestTime = timeNeededtoReachMaxActionBar;
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].IsDead)
                    continue;

                timeNeededtoReachMaxActionBar = (1f - Enemy[i].CurActionBar) / (Enemy[i].CurSpeed / 100f);

                if (timeNeededtoReachMaxActionBar < shortestTime)
                    shortestTime = timeNeededtoReachMaxActionBar;
            }            
            
            return shortestTime;
        }

        IEnumerator AnimateSpeed (List<Character> Ally, List<Character> Enemy, BattleController owner)
        {
            float percent = 0;

            float[] startPosAlly = new float[Ally.Count];
            float[] startPosEnemy = new float[Enemy.Count];

            for (int i = 0; i < Ally.Count; i++)
                startPosAlly[i] = Ally[i].CurActionBar;

            for (int i = 0; i < Enemy.Count; i++)
                startPosEnemy[i] = Enemy[i].CurActionBar;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / duration;

                for (int i = 0; i < Ally.Count; i++)
                {
                    if (!Ally[i].IsDead)
                    {
                        Ally[i].CurActionBar = Mathf.Lerp (startPosAlly[i], startPosAlly[i] + Ally[i].Speed / 100f * duration, percent);
                        owner.OnCharacterActionBarChangedCallback.Invoke ();
                    }
                        
                }

                for (int i = 0; i < Enemy.Count; i++)
                {
                    if (!Enemy[i].IsDead)
                    {
                        Enemy[i].CurActionBar = Mathf.Lerp (startPosEnemy[i], startPosEnemy[i] + Enemy[i].Speed / 100f * duration, percent);
                        owner.OnCharacterActionBarChangedCallback.Invoke ();
                    }                        
                }

                yield return null;
            }
        }

        void CheckWhichCharacterActionBarReachMax (List<Character> Ally, List<Character> Enemy, BattleController owner)
        {
            for (int i = 0; i < Ally.Count; i++)
            {
                if (Ally[i].CurActionBar == 1)
                {
                    owner.fastestCharacterIndex = i;
                    Debug.Log ("It is " + Ally[owner.fastestCharacterIndex].Name + " Turn. [Player]");
                    owner.isPlayerTurn = true;
                    owner.stateMachine.ChangeState (PreparationBeforePerformingCharacterAction.Instance);

                    return;
                }
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].CurActionBar == 1)
                {
                    owner.fastestCharacterIndex = i;
                    Debug.Log ("It is " + Enemy[owner.fastestCharacterIndex].Name + " Turn. [Enemy]");
                    owner.isPlayerTurn = false;
                    owner.stateMachine.ChangeState (PreparationBeforePerformingCharacterAction.Instance);

                    return;
                }
            }            
        }
    }
}