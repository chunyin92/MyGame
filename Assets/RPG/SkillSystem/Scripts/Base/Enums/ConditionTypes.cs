namespace RPG.SkillSystem
{
    [System.Serializable]
    public enum ConditionTypes
    {
        // before action
        Probablility                                = 0,

        WhenHpWithinPercentage,

        SelfAttackLargerThanTargetAttack,
        SelfDefenseLargerThanTargetDefense,

        IfSelfIsStuned,
        IfSelfIsBurnt,
        IfSelfIsFreezed,
        IfSelfIsParalyzed,
        IfSelfIsPoisoned,

        IfTargetIsStuned,
        IfTargetIsBurnt,
        IfTargetIsFreezed,
        IfTargetIsParalyzed,
        IfTargetIsPoisoned,



        // after action
        WhenKilledTarget,
        WhenCriticalHit,
        
    }
}
