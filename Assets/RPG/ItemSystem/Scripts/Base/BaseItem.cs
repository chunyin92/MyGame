using UnityEngine;
using System.Collections;
using UnityEditor;

namespace RPG.ItemSystem
{
    [System.Serializable]
    public class BaseItem : IBaseItem
    {
        #region SerializeField
        [SerializeField] string _name;
        [SerializeField] Sprite _icon;
        [SerializeField] string _description;
        [SerializeField] Quality _quality;
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

        public Quality Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }
        #endregion

        #region Constructors
        public BaseItem()
        {
            _name = string.Empty;
            _icon = new Sprite();
            _description = string.Empty;
            _quality = new Quality();
        }

        public BaseItem(BaseItem baseItem)
        {
            Clone(baseItem);
        }

        public void Clone(BaseItem baseItem)
        {
            _name = baseItem.Name;
            _icon = baseItem.Icon;
            _description = baseItem.Description;
            _quality = baseItem.Quality;
        }
        #endregion

            // Another class

        public virtual void OnGUI()
        {
            LoadQualityDatabase();

            GUILayout.BeginVertical();
            {
                _name = EditorGUILayout.TextField("Name", _name);
                _icon = EditorGUILayout.ObjectField("Icon", _icon, typeof(Sprite), false) as Sprite;
                _description = EditorGUILayout.TextField("Description", _description);
                DisplayQuality();
            }
            GUILayout.EndVertical();
        }

        QualityDatabase qualitydb;
        int selectedQualityIndex = 0;
        string[] options;
        public int SelectedQualityID
        {
            get { return selectedQualityIndex; }
        }

        public void LoadQualityDatabase()
        {
            qualitydb = Resources.Load("Database/QualityDatabase") as QualityDatabase;

            if (qualitydb == null)
                Debug.LogWarning("Skill Type Database not loaded");

            options = new string[qualitydb.Count];

            for (int i = 0; i < qualitydb.Count; i++)
            {
                options[i] = qualitydb.Get(i).Name;
            }
        }

        public void DisplayQuality()
        {
            int itemIndex = 0;

            if (_quality != null)
                itemIndex = qualitydb.GetIndex(_quality.Name);

            if (itemIndex == -1)
                itemIndex = 0;


            selectedQualityIndex = EditorGUILayout.Popup("Quality", itemIndex, options);
            _quality = qualitydb.Get(SelectedQualityID);
        }
    }
}
