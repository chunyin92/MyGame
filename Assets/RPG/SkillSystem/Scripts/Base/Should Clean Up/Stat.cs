using UnityEngine;

namespace RPG.CharacterSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] int _baseValue;            // base value, each character should have their own base value
        //[SerializeField] int _currentMaxValue;      // max value, depends on level and base value
        //[SerializeField] int _currentValue;         // current value, current HP, current attack power, etc.

        public int BaseValue
        {
            get { return _baseValue; }
            set { _baseValue = value; }
        }

        //public int CurrentMaxValue
        //{
        //    get { return _currentMaxValue; }
        //    set { _currentMaxValue = value; }
        //}

        //public int CurrentValue
        //{
        //    get { return _currentValue; }
        //    set { _currentValue = value; }
        //}

        public Stat ()
        {
            _baseValue = 0;
            //_currentMaxValue = 0;
            //_currentValue = 0;
        }

        public Stat (Stat stat)
        {
            _baseValue = stat.BaseValue;
            //_currentMaxValue = stat.CurrentMaxValue;
            //_currentValue = stat.CurrentValue;
        }

        public int GetStatValueWithLevel (int level)
        {
            return (int)(1000 + BaseValue * level / 100f);
        }




        public void AddToBaseValue (int amt)
        {
            _baseValue += amt;
        }

        //public int CurrentAttributeValue
        //{
        //    get { return _currentValue; }
        //    set { _currentValue = value; }
        //}

        //public void AddToCurrentValueByPercent (int percentage, int duration)
        //{
        //    _currentValue = _currentMaxValue * percentage;
        //}

        //public void CalculateStatBasedOnLevel (int level)
        //{
        //    //Debug.Log (CurrentMaxValue);
        //    //Debug.Log (_baseValue);
        //    //Debug.Log (level);
        //    CurrentMaxValue = _baseValue * level;

        //    // TODO need to load character current value
        //    CurrentValue = CurrentMaxValue;

        //    //Debug.Log (CurrentMaxValue);

        //}







    }
}
