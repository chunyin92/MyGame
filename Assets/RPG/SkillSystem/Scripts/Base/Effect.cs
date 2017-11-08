using UnityEngine;
using RPG.CharacterSystem;
using System.Collections.Generic;

namespace RPG.SkillSystem
{
    // effect should be used for character, skill and item system, should we put to general ststem???
    [System.Serializable]
    public class Effect
    {
        #region SerializeField
        [SerializeField] ConditionTypes _conditionType;
        [SerializeField] int _probability;
        [SerializeField] TargetTypes _targetType;
        [SerializeField] EffectTypes _effectType;
        [SerializeField] int _percentage;
        [SerializeField] int _duration;
        #endregion

        #region Setters and getters
        public ConditionTypes ConditionType
        {
            get { return _conditionType; }
            set { _conditionType = value; }
        }

        public int Probability
        {
            get { return _probability; }
            set { _probability = value; }
        }

        public TargetTypes TargetType
        {
            get { return _targetType; }
            set { _targetType = value; }
        }

        public EffectTypes EffectType
        {
            get { return _effectType; }
            set { _effectType = value; }
        }

        public int Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        #endregion

        #region Constructors
        public Effect()
        {
            _conditionType = 0;
            _probability = 0;
            _targetType = 0;
            _effectType = 0;            
            _percentage = 0;
            _duration = 1;
        }

        public Effect (Effect effect)
        {
            _conditionType = effect.ConditionType;
            _probability = effect.Probability;
            _targetType = effect.TargetType;
            _effectType = effect.EffectType;
            _percentage = effect.Percentage;
            _duration = effect.Duration;
        }
        #endregion

        #region Public function        
        // character should have a status property for applying effect???
        
        public bool IsEffectPreMove ()
        {
            if ((int)_conditionType < 500)
                return true;

            return false;
        }

        public bool IsConditionMet (Character self, Character target)
        {
            switch (_conditionType)
            {
                case ConditionTypes.WhenHpWithinPercentage:
                    if ((float)self.CurHP / self.MaxHP < _probability)
                        return true;
                    return false;
                case ConditionTypes.Probablility:
                    if (Random.Range (1, 100) < _probability)
                        return true;
                    return false;
                case ConditionTypes.SelfAttackLargerThanTargetAttack:
                    return IsFormerStatLargerThanLaterStat (self.CurAttack, target.CurAttack);
                case ConditionTypes.SelfDefenseLargerThanTargetDefense:
                    return IsFormerStatLargerThanLaterStat (self.CurDefense, target.CurDefense);


            }

            return false;
        }


        public void ApplyEffect (Character curCharacter, List<Character> Ally, Character curTarget, List<Character> Enemy)
        {
            switch (_targetType)
            {
                case TargetTypes.SINGLE:
                    AddStatusToSingleCharacter (curTarget);
                    break;
                case TargetTypes.ALL_ENEMIES:
                    AddStatusToAllCharacters (Enemy);
                    break;
                case TargetTypes.SELF:
                    AddStatusToSingleCharacter (curCharacter);
                    break;
                case TargetTypes.ALL_ALLIES:
                    AddStatusToAllCharacters (Ally);
                    break;
                default:
                    Debug.Log ("Have not yet implement " + _targetType.ToString ());
                    break;
            }
        }
        #endregion

        #region Private function      
        bool IsFormerStatLargerThanLaterStat (int selfStat, int targetStat)
        {
            Debug.Log (selfStat + " VS " + targetStat);

            if (selfStat > targetStat)
                return true;
            else
                return false;
        }

        void AddStatusToSingleCharacter (Character character)
        {
            if (character.IsDead)
                return;

            if (character.Status == null)
                Debug.LogWarning (character.Name + " status is null");
            
            character.Status.Add (new Status (this));
        }

        void AddStatusToAllCharacters (List<Character> characters)
        {
            foreach (Character character in characters)
            {
                AddStatusToSingleCharacter (character);
            }                
        }
        #endregion
    }
}
