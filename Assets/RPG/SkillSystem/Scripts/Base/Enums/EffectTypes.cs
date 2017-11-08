namespace RPG.SkillSystem
{
    //[System.Serializable]
    public enum EffectTypes
    {
        // Modify Stats                     : 0 - 99
        ModifyAttack                        = 0,
        ModifyDefense                       = 1,
        ModifySpeed                         = 2,
        ModifyCritcalHitRate                = 3,
        ModifyCritcalHitDamage              = 4,
        ModifyAccuracy                      = 5,
                
        IncreaseDamageDeal                  = 50,
        DecreaeDamageDeal                   = 51,
        IncreaseDamageReceived              = 52,
        DecreaseDamageReceived              = 53,

        // Status Effects                   : 100 - 199
        Heal                                = 100,
        Stun                                = 101,
        Burn                                = 102,
        Freeze                              = 103,
        Paralysis                           = 104,
        Poison                              = 105,
        Shield,

        // Special Effects                  : 200 - 299
        RemoveDebuff                        = 200,
        RemoveBuff                          = 201,
        RestoreHealthBasedOnDamageDealt     = 203,
        BlockDamage,
        DamageSelf,

        //
        ImmuneToStun
    }
}
