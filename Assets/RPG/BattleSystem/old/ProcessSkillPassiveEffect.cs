using UnityEngine;
using System.Collections;
using RPG.CharacterSystem;
using System.Collections.Generic;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        //void ProcessSkillPassiveEffect(List<Character> Attacker, List<Character> Defender)
        //{
        //    // iterate all effects from the selected skill
        //    for (int i = 0; i < Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect.Count; i++)
        //    {
        //        // determine skill effect target type
        //        switch (Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect[i].TargetType)
        //        {
        //            case SkillSystem.TargetTypes.SINGLE:
        //                Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect[i].ApplyEffect (Defender[selectedTargetIndex]);
        //                Defender[selectedTargetIndex].ProcessStatus ();                        
        //                break;
        //            case SkillSystem.TargetTypes.ALL_ENEMIES:
        //                Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect[i].ApplyEffect (Defender);

        //                foreach (Character e in Enemy)
        //                {
        //                    e.ProcessStatus ();
        //                }

        //                break;
        //            case SkillSystem.TargetTypes.SELF:
        //                Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect[i].ApplyEffect (Attacker[fastestCharacterIndex]);
        //                Attacker[fastestCharacterIndex].ProcessStatus ();
        //                break;
        //            case SkillSystem.TargetTypes.ALL_ALLIES:
        //                Attacker[fastestCharacterIndex].Skill[selectedSkillIndex].Effect[i].ApplyEffect (Attacker);

        //                foreach (Character a in Ally)
        //                {
        //                    a.ProcessStatus ();
        //                }

        //                break;
        //            case SkillSystem.TargetTypes.DOUBLE:
        //                // need to implentment later
        //                break;

                        
        //        }
        //    }
        //}
    }
}
