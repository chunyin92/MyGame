using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
    [System.Serializable]
    public class Quality : IQuality
    {
        #region SerializeField
        [SerializeField] string _name;
        [SerializeField] Sprite _icon;
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
        #endregion

        #region Constructors
        public Quality()
        {
            _name = string.Empty;
            _icon = new Sprite();
        }

        public Quality(string name, Sprite icon)
        {
            _name = name;
            _icon = icon;
        }
        #endregion
    }
}
