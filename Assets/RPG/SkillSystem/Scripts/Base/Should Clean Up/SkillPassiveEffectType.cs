using UnityEngine;

namespace RPG.SkillSystem
{
    [System.Serializable]
    public class SkillPassiveEffectType : ISkillPassiveEffectType
    {
        #region SerializeField
        [SerializeField]
        string _name;
        #endregion

        #region Setters and getters
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Constructors
        public SkillPassiveEffectType()
        {
            Name = string.Empty;
        }
        #endregion                
    }
}