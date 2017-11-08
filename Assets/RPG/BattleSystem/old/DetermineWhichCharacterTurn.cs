using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        int maxSpeed = -1;
        int fastestCharacterIndex = -1;

        int randomNumber;

        bool isPlayerTurn;

        List<int> fastestAllyIndex = new List<int>();
        List<int> fastestEnemyIndex = new List<int>();

        
        bool hasStarted = false;
        float shortestTime = 100;

        IEnumerator AnimateSpeed (float time)
        {
            float duration = time;

            float percent = 0;

            float[] startPosAlly = new float[Ally.Count];
            float[] startPosEnemy = new float[Enemy.Count];

            for (int i = 0; i < Ally.Count; i++)
            {
                startPosAlly[i] = Ally[i].CurActionBar;
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                startPosEnemy[i] = Enemy[i].CurActionBar;
            }

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / duration;


                //TimeLine[0].value = Mathf.Lerp (startPosAlly[0], startPosAlly[0] + Ally[0].Speed / 100f * duration, percent);
                //TimeLine[1].value = Mathf.Lerp (startPosAlly[1], startPosAlly[1] + Ally[1].Speed / 100f * duration, percent);


                for (int i = 0; i < Ally.Count; i++)
                {
                    if (!Ally[i].IsDead)
                        Ally[i].CurActionBar = TimeLine[i].value = Mathf.Lerp (startPosAlly[i], startPosAlly[i] + Ally[i].Speed / 100f * duration, percent);
                }

                for (int i = 0; i < Enemy.Count; i++)
                {
                    if (!Enemy[i].IsDead)
                        Enemy[i].CurActionBar = TimeLine[i + 2].value = Mathf.Lerp (startPosEnemy[i], startPosEnemy[i] + Enemy[i].Speed/100f * duration, percent);

                    //Enemy[i].CurActionBar = TimeLine[i + 2].value = Mathf.Lerp (startPosEnemy[i], startPosEnemy[i] + Enemy[i].Speed * duration, percent);
                }
                


                yield return null;
            }




            




            //float duration = time;

            //float percent = 0;

            //float a0 = TimeLine[0].value;
            //float a1 = TimeLine[1].value;

            //while (percent < 1)
            //{
            //    percent += Time.deltaTime * 1 / duration;

            //    TimeLine[0].value = Mathf.Lerp (a0, a0 + increaseRate[0] * duration, percent);                
            //    TimeLine[1].value = Mathf.Lerp (a1, a1 + increaseRate[1] * duration, percent);
            


            //    yield return null;
            //}

        }


        void DetermineWhichCharacterTurn()
        {
            

            if (!hasStarted)
            {
                selectedSkillIndex = -1;
                selectedTargetIndex = -1;
                fastestCharacterIndex = -1;


                shortestTime = 100;

                shortestTime = GetShortestTime ();

                //for (int i = 0; i < 2; i++)
                //{
                //    float timeNeededToReachTo1 = (1f - TimeLine[i].value) / increaseRate[i];


                //    Debug.Log ("1 - " + TimeLine[i].value + " / " + increaseRate[i]);
                //    Debug.Log ("Time for " + i + " : " + timeNeededToReachTo1);


                //    //if (reach < 0) reach = 0;
                //    if (timeNeededToReachTo1 < shortestTime)
                //    {
                //        shortestTime = timeNeededToReachTo1;

                //        fastestCharacterIndex = i;
                //    }
                //}

                //Debug.Log ("fastest is " + fastestCharacterIndex);
                
                hasStarted = true;
                Debug.Log ("shortest time " + shortestTime);
                StartCoroutine (AnimateSpeed (shortestTime));                
            }

            //Debug.Log (Ally[1].CurActionBar);
            for (int i = 0; i < Ally.Count; i++)
            {
                if (Ally[i].CurActionBar == 1)
                {
                    fastestCharacterIndex = i;
                    isPlayerTurn = true;
                }
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].CurActionBar == 1)
                {
                    fastestCharacterIndex = i;
                    isPlayerTurn = false;
                }
            }

            if (fastestCharacterIndex!=-1)
                if (isPlayerTurn)
                {
                    Debug.Log ("It is " + Ally[fastestCharacterIndex].Name + " Turn. [Player]");
                    Ally[fastestCharacterIndex].CurActionBar = 0;
                    TimeLine[fastestCharacterIndex].value = 0;
                    hasStarted = false;
                    currentBattleState = BattleState.PLAYER_CHOICE;
                }
                else
                {
                    Debug.Log ("It is " + Enemy[fastestCharacterIndex].Name + " Turn. [Enemy]");
                    Enemy[fastestCharacterIndex].CurActionBar = 0;
                    TimeLine[fastestCharacterIndex + Ally.Count].value = 0;
                    hasStarted = false;
                    currentBattleState = BattleState.ENEMY_CHOICE;
                }



            //RefreshVariable ();
            //GetMaxSpeed();
            //GetCharacterWithMaxSpeed();
            //GetFastestCharacterIndex();

            //if (isPlayerTurn)
            //{
            //    Debug.Log("It is " + Ally[fastestCharacterIndex].Name + " Turn. [Player]");
            //    currentBattleState = BattleState.PLAYER_CHOICE;
            //}
            //else
            //{
            //    Debug.Log("It is " + Enemy[fastestCharacterIndex].Name + " Turn. [Enemy]");
            //    currentBattleState = BattleState.ENEMY_CHOICE;
            //}

        }

        float GetShortestTime ()
        {
            shortestTime = 100;

            for (int i = 0; i < Ally.Count; i++)
            {
                float timeNeededToReachTo1 = (1f - Ally[i].CurActionBar) / (Ally[i].CurSpeed / 100f);

                if (timeNeededToReachTo1 < shortestTime && !Ally[i].IsDead)
                    shortestTime = timeNeededToReachTo1;
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                float timeNeededToReachTo1 = (1f - Enemy[i].CurActionBar) / (Enemy[i].CurSpeed / 100f);

                if (timeNeededToReachTo1 < shortestTime && !Enemy[i].IsDead)
                    shortestTime = timeNeededToReachTo1;
            }

            return shortestTime;
        }

        





        void RefreshVariable()
        {
            selectedSkillIndex = -1;
            selectedTargetIndex = -1;

            maxSpeed = -1;
            fastestCharacterIndex = -1;
            fastestAllyIndex.Clear();
            fastestEnemyIndex.Clear();
        }

        void GetMaxSpeed()
        {
            for (int i = 0; i < Ally.Count; i++)
            {
                if (Ally[i].CurSpeed > maxSpeed && !Ally[i].IsDead && !Ally[i].IsMoved)
                    maxSpeed = Ally[i].CurSpeed;
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].CurSpeed > maxSpeed && !Enemy[i].IsDead && !Enemy[i].IsMoved)
                    maxSpeed = Enemy[i].CurSpeed;
            }
        }

        void GetCharacterWithMaxSpeed()
        {
            for (int i = 0; i < Ally.Count; i++)
            {
                if (Ally[i].CurSpeed == maxSpeed && !Ally[i].IsDead && !Ally[i].IsMoved)
                {
                    fastestAllyIndex.Add(i);
                }
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].CurSpeed == maxSpeed && !Enemy[i].IsDead && !Enemy[i].IsMoved)
                {
                    fastestEnemyIndex.Add(i);
                }
            }
        }

        void GetFastestCharacterIndex()
        {
            if (fastestAllyIndex.Count + fastestEnemyIndex.Count == 1)
            {
                if (fastestAllyIndex.Count == 1)
                {
                    fastestCharacterIndex = fastestAllyIndex[0];
                    isPlayerTurn = true;
                }
                else
                {
                    fastestCharacterIndex = fastestEnemyIndex[0];
                    isPlayerTurn = false;
                }
            }
            else
            {
                randomNumber = Random.Range(0, fastestAllyIndex.Count + fastestEnemyIndex.Count);

                Debug.Log("Random no. " + randomNumber);

                if (randomNumber < fastestAllyIndex.Count)
                {
                    fastestCharacterIndex = fastestAllyIndex[randomNumber];
                    isPlayerTurn = true;
                }
                else
                {
                    fastestCharacterIndex = fastestEnemyIndex[randomNumber - fastestAllyIndex.Count];
                    isPlayerTurn = false;
                }
            }
        }
    }
}
