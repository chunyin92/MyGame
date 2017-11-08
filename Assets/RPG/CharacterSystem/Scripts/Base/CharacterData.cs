using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using RPG.SkillSystem;

namespace RPG.CharacterSystem
{
    [System.Serializable]
    public class CharacterData
    {
        #region SerializeField
        [SerializeField] string _name;
        [SerializeField] Sprite _icon;
        [SerializeField] GameObject _prefab;
        [SerializeField] string _description;
        [SerializeField] int _baseHP;
        [SerializeField] int _baseMP;
        [SerializeField] int _baseAttack;
        [SerializeField] int _baseDefense;
        [SerializeField] int _basetalent;
        [SerializeField] int _baseSpeed;
        [SerializeField] int _baseCriticalRate;
        [SerializeField] int _baseCriticalDamage;
        [SerializeField] List<SkillData> _skillData;
        #endregion

        #region Setters ans getters
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

        public GameObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int BaseHP
        {
            get { return _baseHP; }
            set { _baseHP = value; }
        }

        public int BaseMP
        {
            get { return _baseMP; }
            set { _baseMP = value; }
        }

        public int BaseAttack
        {
            get { return _baseAttack; }
            set { _baseAttack = value; }
        }

        public int BaseDefense
        {
            get { return _baseDefense; }
            set { _baseDefense = value; }
        }

        public int BaseTalent
        {
            get { return _basetalent; }
            set { _basetalent = value; }
        }

        public int BaseSpeed
        {
            get { return _baseSpeed; }
            set { _baseSpeed = value; }
        }

        public int BaseCriticalRate
        {
            get { return _baseCriticalRate; }
            set { _baseCriticalRate = value; }
        }

        public int BaseCriticalDamage
        {
            get { return _baseCriticalDamage; }
            set { _baseCriticalDamage = value; }
        }

        public List<SkillData> SkillData
        {
            get { return _skillData; }
            set { _skillData = value; }
        }
        #endregion

        #region Constructors
        public CharacterData()
        {
            _name = string.Empty;
            _icon = new Sprite();
            //prefab
            //_prefab = new GameObject ();
            _description = string.Empty;
            _baseHP = 0;
            _baseMP = 0;
            _baseAttack = 0;
            _baseDefense = 0;
            _basetalent = 0;
            _baseSpeed = 0;
            _baseCriticalRate = 0;
            _baseCriticalDamage = 0;
            _skillData = new List<SkillData>();
        }

        public CharacterData(CharacterData character)
        {
            Clone(character);
        }

        public void Clone(CharacterData character)
        {
            _name = character.Name;
            _icon = character.Icon;
            _prefab = character.Prefab;
            _description = character.Description;
            _baseHP = character.BaseHP;
            _baseMP = character.BaseMP;
            _baseAttack = character.BaseAttack;
            _baseDefense = character.BaseDefense;
            _basetalent = character.BaseTalent;
            _baseSpeed = character.BaseSpeed;
            _baseCriticalRate = character.BaseCriticalRate;
            _baseCriticalDamage = character.BaseCriticalDamage;

            _skillData = new List<SkillData> ();

            for (int i = 0; i < character.SkillData.Count; i++)
                _skillData.Add (new SkillData (character.SkillData[i]));
        }
        #endregion
    }
}
