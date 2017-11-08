using UnityEngine;
using System.Collections;
using RPG.CharacterSystem;
using System.Collections.Generic;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        int dmg;

        void ProcessAction()
        {
            if (isPlayerTurn)
            {
                //ProcessSkillPassiveEffect(Ally, Enemy);
                CalculateDamage(Ally, Enemy);
            }
            else
            {
                //CalculateDamage(Enemy, Ally);
            }



            

        }


        void CalculateDamage(List<Character> Attacker, List<Character> Defender)
        {
            Debug.Log("Attack " + Attacker[fastestCharacterIndex].CurAttack);
            Debug.Log("Defense " + Defender[selectedTargetIndex].CurDefense);
            Debug.Log("Base Power " + Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].BasePower);

            dmg = (int)((float)Attacker[fastestCharacterIndex].CurAttack / Defender[selectedTargetIndex].CurDefense * Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].BasePower);
            

            if (IsCriticalHit(Attacker[fastestCharacterIndex].CurCriticalRate))
            {
                Debug.Log("Critcal Hit!");
                Debug.Log("Damage: " + dmg);
                dmg += (int)(dmg * (Attacker[fastestCharacterIndex].CurCiticalDamage / 100f));
                Debug.Log(Attacker[fastestCharacterIndex].CurCiticalDamage);
                Debug.Log("Crit Damage: " + dmg);
            }

            Defender[selectedTargetIndex].CurHP -= dmg;


            if (Defender[selectedTargetIndex].CurHP <= 0)
            {
                Defender[selectedTargetIndex].IsDead = true;
                Destroy(Defender[selectedTargetIndex].Prefab);
            }
            

            currentBattleState = BattleState.DETERMINE_NEXT_STATE;
        }

        bool IsCriticalHit(int criticalRate)
        {
            int randomNumber = Random.Range(0, 100);
            Debug.Log("Random number: " + randomNumber + " Rate: " + Ally[fastestCharacterIndex].CurCriticalRate);
            if (randomNumber < criticalRate)
                return true;
            else
                return false;
        } 






    }
}
