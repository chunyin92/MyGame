using UnityEngine;
using System.Collections.Generic;
using RPG.CharacterSystem;

namespace RPG.SkillSystem
{
    [System.Serializable]
    public class Skill
    {
        #region SerializeField
        [SerializeField] string _name;
        [SerializeField] Sprite _icon;
        [SerializeField] string _description;
        [SerializeField] int _basePower;
        [SerializeField] int _cooldown;
        [SerializeField] int _manaCost;
        [SerializeField] int _accuarcy;
        [SerializeField] TargetTypes _targetType;
        [SerializeField] List<Effect> _effect;
        [SerializeField] int _cooldownCounter;
        [SerializeField] bool _isOnCooldown;
        [SerializeField] bool _isLearned;
        // skill level???
        #endregion

        #region Setters and getters
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int BasePower
        {
            get { return _basePower; }
            set { _basePower = value; }
        }

        public int Cooldown
        {
            get { return _cooldown; }
            set { _cooldown = value; }
        }

        public int ManaCost
        {
            get { return _manaCost; }
            set { _manaCost = value; }
        }

        public int Accuracy
        {
            get { return _accuarcy; }
            set { _accuarcy = value; }
        }

        public TargetTypes TargetType
        {
            get { return _targetType; }
            set { _targetType = value; }
        }

        public List<Effect> Effect
        {
            get { return _effect; }
            set { _effect = value; }
        }

        public int CooldownCounter
        {
            get { return _cooldownCounter; }
            set { _cooldownCounter = value; }
        }

        public bool IsOnCooldown
        {
            get { return _isOnCooldown; }
            set { _isOnCooldown = value; }
        }

        public bool IsLearn
        {
            get { return _isLearned; }
            set { _isLearned = value; }
        }
        #endregion

        #region Constructors
        //public Skill ()
        //{
        //    _skillData = new SkillData ();
        //    _cooldownCounter = 0;
        //    _isOnCooldown = false;
        //}

        public Skill (SkillData skillData)
        {
            _name = skillData.Name;
            _icon = skillData.Icon;
            _description = skillData.Description;
            _basePower = skillData.BasePower;
            _cooldown = skillData.Cooldown;
            _manaCost = skillData.ManaCost;
            _accuarcy = skillData.Accuracy;
            _targetType = skillData.TargetType;

            for (int i = 0; i < skillData.Effect.Count; i++)
            {
                if (_effect == null)
                    _effect = new List<Effect> ();

                _effect.Add (new Effect (skillData.Effect[i]));
            }

            _cooldownCounter = 0;
            _isOnCooldown = false;
            _isLearned = true;
        }
        #endregion       

        /// <summary>
        /// Set Cool Down Counter to Skill's cool down and set _isOnCooldown to true
        /// </summary>
        public void SetCoolDownCounterToMax ()
        {
            _cooldownCounter = _cooldown;
            _isOnCooldown = true;
        }
    }
}

