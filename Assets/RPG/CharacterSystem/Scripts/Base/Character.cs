using UnityEngine;
using RPG.SkillSystem;
using System.Collections.Generic;

namespace RPG.CharacterSystem
{
    [System.Serializable]
    public class Character
    {
        #region SerializeField

        #region Character Data
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
        [SerializeField] List<Skill> _skill;
        #endregion

        [SerializeField] int _level;        

        [SerializeField] int _maxHP;
        [SerializeField] int _maxMP;   
        [SerializeField] int _attack;
        [SerializeField] int _defense;
        [SerializeField] int _talent;
        [SerializeField] int _speed;

        [SerializeField] int _criticalRate;
        [SerializeField] int _criticalDamage;


        #region battle Related
        [SerializeField] int _curHP;
        [SerializeField] int _curMP;
        [SerializeField] int _curAttack;
        [SerializeField] int _curDefense;
        [SerializeField] int _curSpeed;

        [SerializeField] int _curCriticalRate;
        [SerializeField] int _curCiticalDamage;

        [SerializeField] int _curAccuracyModifier;
        [SerializeField] int _curDodgeModifier;
        [SerializeField] int _curDamageDealModifier;
        [SerializeField] int _curDamageReceivedModifier;

        [SerializeField] bool _isMoved;
        [SerializeField] bool _isDead;
        [SerializeField] float _curActionBar;

        [SerializeField] List<Status> _status;
        #endregion
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

        public List<Skill> Skill
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int MaxHP
        {
            get { return _maxHP; }
            set { _maxHP = value; }
        }

        public int MaxMP
        {
            get { return _maxMP; }
            set{ _maxMP = value; }
        }

        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        public int Talent
        {
            get { return _talent; }
            set { _talent = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int CriticalRate
        {
            get { return _criticalRate; }
            set { _criticalRate = value; }
        }

        public int CriticalDamage
        {
            get { return _criticalDamage; }
            set { _criticalDamage = value; }
        }

        public int CurHP
        {
            get { return _curHP; }
            set
            {
                if (value < 0)
                    _curHP = 0;
                else if (value > _maxHP)
                    _curHP = _maxHP;
                else
                    _curHP = value;
            }
        }

        public int CurMP
        {
            get { return _curMP; }
            set {
                if (value < 0)
                    _curMP = 0;
                else if (value > _maxMP)
                    _curMP = _maxMP;
                else
                    _curMP = value;
            }
        }

        public int CurAttack
        {
            get { return _curAttack; }
            set { _curAttack = value; }
        }

        public int CurDefense
        {
            get { return _curDefense; }
            set { _curDefense = value; }
        }

        public int CurSpeed
        {
            get { return _curSpeed; }
            set { _curSpeed = value; }
        }

        public int CurCriticalRate
        {
            get { return _curCriticalRate; }
            set { _curCriticalRate = value; }
        }

        public int CurCiticalDamage
        {
            get { return _curCiticalDamage; }
            set { _curCiticalDamage = value; }
        }

        public int CurAccuracyModifier
        {
            get { return _curAccuracyModifier; }
            set { _curAccuracyModifier = value; }
        }

        public int CurDodgeModifier
        {
            get { return _curDodgeModifier; }
            set { _curDodgeModifier = value; }
        }

        public int CurDamageDealModifier
        {
            get { return _curDamageDealModifier; }
            set { _curDamageDealModifier = value; }
        }

        public int CurDamageReceivedModifier
        {
            get { return _curDamageReceivedModifier; }
            set { _curDamageReceivedModifier = value; }
        }
        
        public bool IsMoved
        {
            get { return _isMoved; }
            set { _isMoved = value; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        public float CurActionBar
        {
            get { return _curActionBar; }
            set { _curActionBar = value; }
        }

        public List<Status> Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion

        #region Constructors
        public Character (CharacterData characterData, int level)
        {
            _name = characterData.Name;
            _icon = characterData.Icon;
            _prefab = characterData.Prefab;
            _description = characterData.Description;
            _baseHP = characterData.BaseHP;
            _baseMP = characterData.BaseMP;
            _baseAttack = characterData.BaseAttack;
            _baseDefense = characterData.BaseDefense;
            _basetalent = characterData.BaseTalent;
            _baseSpeed = characterData.BaseSpeed;
            _baseCriticalRate = characterData.BaseCriticalRate;
            _baseCriticalDamage = characterData.BaseCriticalDamage;

            _skill = new List<Skill> ();
            for (int i = 0; i < characterData.SkillData.Count; i++)
            {
                _skill.Add (new Skill (characterData.SkillData[i]));
            }

            _level = level;
            _status = new List<Status> ();

            // TODO: rework later
            _maxHP = CalculateStat (characterData.BaseHP, level);
            _maxMP = CalculateStat (characterData.BaseMP, level);
            _attack = CalculateStat (characterData.BaseAttack, level);
            _defense = CalculateStat (characterData.BaseDefense, level);
            _talent = CalculateStat (characterData.BaseTalent, level);
            _speed = characterData.BaseSpeed;
            _criticalRate = characterData.BaseCriticalRate;
            _criticalDamage = characterData.BaseCriticalDamage;
                        
            _curHP = _maxHP;
            _curMP = _maxMP;
            _curAttack = _attack;
            _curDefense = _defense;
            // talent???
            _curSpeed = _speed;
            _curCriticalRate = _criticalRate;
            _curCiticalDamage = _criticalDamage;

            CurAccuracyModifier = 0;
            CurDodgeModifier = 0;
            _curDamageDealModifier = 0;
            _curDamageReceivedModifier = 0;
            
            _isMoved = false;
            _isDead = false;
            _curActionBar = 0;
        }
        #endregion

        #region Methods
        int CalculateStat (int baseStat, int level)
        {
            return baseStat;
        }
        #endregion

        public void ProcessStatus ()
        {
            for (int i = 0; i < Status.Count; i++)
            {

                Debug.Log ("name: " + Status[i].EffectType.ToString ());
                Debug.Log ("duration: " + Status[i].Duration.ToString ());
                //for EffectTypes enum 0 - 99
                if ((int)Status[i].EffectType > -1 && (int)Status[i].EffectType< 100)
                    switch (Status[i].EffectType)
                    {
                        case EffectTypes.ModifyAttack:
                            Debug.Log ("************************************* " + CurAttack);
                            Debug.Log ("************************************* " + Status[i].Percentage);
                            CurAttack += Attack * Status[i].Percentage / 100;
                            Debug.Log ("************************************* " + CurAttack);
                            break;

                    }
            }
        }


        /// <summary>
        /// Set Character _isDead to true and update _curActionBar to 0
        /// </summary>
        public void SetCharacterStatusToDead ()
        {
            _isDead = true;
            _curActionBar = 0;
            Object.Destroy (Prefab);
        }

        public void MinusAllStatusDurationByOne ()
        {
            for (int i = 0; i < _status.Count; i++)
            {
                if (_status[i].Duration > 0)
                    _status[i].Duration--;

                if (_status[i].Duration == 0)
                    _status.Remove (_status[i]);
            }
        }        

        /// <summary>
        /// Subtract Cool Down Counter by 1 and check if it is = 0, if yes, set _isOnCooldown to false
        /// </summary>
        public void MinusAllSkillCoolDownCounterByOne ()
        {
            for (int i = 0; i < _skill.Count; i++)
            {
                if (_skill[i].CooldownCounter > 0)
                    _skill[i].CooldownCounter--;

                if (_skill[i].CooldownCounter == 0)
                    _skill[i].IsOnCooldown = false;
            }            
        }
    }
}
