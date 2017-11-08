using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SkillSystem;

namespace RPG.CharacterSystem
{
    [System.Serializable]
    public class Status
    {
        [SerializeField] EffectTypes _effectType;
        [SerializeField] int _percentage;
        [SerializeField] int _duration;

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
        
        public Status ()
        {
            _effectType = 0;
            _percentage = 0;
            _duration = 1;
        }

        public Status (Effect effect)
        {
            _effectType = effect.EffectType;
            _percentage = effect.Percentage;
            _duration = effect.Duration;
        }

        //public void ProcessStatus ()
        //{
        //    // for EffectTypes enum 0 - 99
        //    if ((int)EffectType > -1 && (int)EffectType < 100)
        //        switch (EffectType)
        //        {
        //            case EffectTypes.ATTACK_UP:

        //        }





        //}

        



    }
}
