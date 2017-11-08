using UnityEngine;
using System.Collections.Generic;
using RPG.SkillSystem;

namespace RPG.CharacterSystem
{
    public interface IBaseCharacter
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
        GameObject Prefab { get; }
        string Description { get; set; }
        int BaseHp { get; set; }
        int BaseMp { get; set; }
        int BaseAttack { get; set; }
        int BaseDefense { get; set; }
        int BaseTalent { get; set; }
        int BaseSpeed { get; set; }
        int BaseCriticalRate { get; set; }
        int BaseCriticalDamage { get; set; }
        List<SkillData> BaseSkill { get; set; }
    }
}