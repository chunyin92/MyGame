using UnityEngine;
using System.Collections;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        void DetermineNextState()
        {
            if (AllEnemyDead())
            {
                currentBattleState = BattleState.WIN;
            }
            else if (AllAllyDead())
            {
                currentBattleState = BattleState.LOSE;
            }
            else if (AllAliveCharacterMoved())
            {
                // another turn after all character moved
                currentBattleState = BattleState.TURN_START;

                for (int i = 0; i < Ally.Count; i++)
                {
                    for (int j = 0; j < Ally[i].Status.Count; j++)
                    {
                        Ally[i].Status[j].Duration--;

                        if (Ally[i].Status[j].Duration == 0)
                            Ally[i].Status.Remove (Ally[i].Status[j]);
                    }                    
                }

                for (int i = 0; i < Enemy.Count; i++)
                {
                    for (int j = 0; j < Enemy[i].Status.Count; j++)
                    {
                        Enemy[i].Status[j].Duration--;

                        if (Enemy[i].Status[j].Duration == 0)
                            Enemy[i].Status.Remove (Enemy[i].Status[j]);
                    }
                }


            }
            else if (!AllAliveCharacterMoved())
            {
                currentBattleState = BattleState.DETERMINE_WHICH_CHARACTER_TURN;
            }
            else
            {
                Debug.LogWarning("Check battle state logic");
            }
        }

        bool AllAliveCharacterMoved()
        {
            for (int i = 0; i < Ally.Count; i++)
            {
                if (!Ally[i].IsDead)
                {
                    if (!Ally[i].IsMoved)
                    {
                        return false;
                    }
                }                
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (!Enemy[i].IsDead)
                {
                    if (!Enemy[i].IsMoved)
                    {
                        return false;
                    }
                }                
            }
            return true;            
        }

        bool AllAllyDead()
        {
            for (int i = 0; i < Ally.Count; i++)
            {
                if (!Ally[i].IsDead)
                {
                    return false;
                }
            }
            return true;
        }

        bool AllEnemyDead()
        {
            for (int i = 0; i < Enemy.Count; i++)
            {
                if (!Enemy[i].IsDead)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
