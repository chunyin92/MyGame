using UnityEngine;
using System.Collections.Generic;
using RPG.CharacterSystem;

namespace RPG.SkillSystem
{
    [System.Serializable]
    public class SkillData
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
        #endregion

        #region Constructors
        public SkillData ()
        {
            _name = string.Empty;
            _icon = new Sprite ();
            _description = string.Empty;
            _basePower = 0;
            _cooldown = 0;
            _manaCost = 0;
            _accuarcy = 0;
            _targetType = 0;
            _effect = new List<Effect> ();
        }

        public SkillData (SkillData skillData)
        {
            Clone (skillData);
        }

        public void Clone (SkillData skillData)
        {
            _name = skillData.Name;
            _icon = skillData.Icon;
            _description = skillData.Description;
            _basePower = skillData.BasePower;
            _cooldown = skillData.Cooldown;
            _manaCost = skillData.ManaCost;
            _accuarcy = skillData.Accuracy;
            _targetType = skillData.TargetType;

            if (_effect == null)
                _effect = new List<Effect> ();

            for (int i = 0; i < skillData.Effect.Count; i++)
            {
                //_skillEffect.Add (skill.SkillEffect[i]);

                //if (_effect == null)
                //    _effect = new List<Effect> ();

                _effect.Add (new Effect (skillData.Effect[i]));
            }
        }
        #endregion

        #region Function
        



        //public bool IsIdentical (BaseSkill skill)
        //{
        //    if (_name != skill.Name ||
        //        _icon != skill.Icon ||
        //        _description != skill.Description ||
        //        _basePower != skill.BasePower ||
        //        _cooldown != skill.Cooldown ||
        //        _manaCost != skill.ManaCost ||
        //        _accuarcy != skill.Accuracy ||
        //        _targetType != skill.TargetType)
        //    {
        //        Debug.Log ("a");
        //        return false;
        //    }


        //    for (int i = 0; i < skill.Effect.Count; i++)
        //    {
        //        if (_effect == null)
        //            _effect = new List<Effect> ();

        //        if (_effect[i] != skill.Effect[i])
        //        {
        //            Debug.Log ("b");
        //            return false;
        //        }
        //    }

        //    Debug.Log ("c");
        //    return true;
        //}
        #endregion
    }
}
