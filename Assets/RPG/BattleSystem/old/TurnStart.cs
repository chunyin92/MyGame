using UnityEngine;
using System.Collections;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        int turn = 0;

        void TurnStart()
        {
            turn++;
            Debug.Log("Turn " + turn + ":");

            // Set character ismoved to false if the character is not dead
            for (int i = 0; i < Ally.Count; i++)
            {
                if (!Ally[i].IsDead)
                {
                    Ally[i].IsMoved = false;
                }                
            }

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (!Enemy[i].IsDead)
                {
                    Enemy[i].IsMoved = false;
                }
            }

            currentBattleState = BattleState.DETERMINE_WHICH_CHARACTER_TURN;
        }
    }
}
