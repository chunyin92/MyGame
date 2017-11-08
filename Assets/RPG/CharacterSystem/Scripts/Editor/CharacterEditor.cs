using UnityEngine;
using UnityEditor;
using RPG.SkillSystem;

namespace RPG.CharacterSystem.Editor
{
    public partial class CharacterEditor : EditorWindow
    {
        CharacterData                           _tempCharacterData                                  = new CharacterData ();
        CharacterDatabase                       _characterDatabase;

        SkillDatabase                           _skillDatabase;
        string[]                                _skillOptions;

        const string                            DATABASE_PATH                                   = @"Resources/Database";
        const string                            DATABASE_NAME                                   = @"CharacterDatabase.asset";        

        Vector2                                 _scrollPos                                      = Vector2.zero;
        int                                     _listViewWidth                                  = 250;
        int                                     _listViewButtonWidth                            = 190;
        int                                     _listViewButtonHieght                           = 25;
        int                                     _labelWidth                                     = 150;
        int                                     _selectedIndex                                  = -1;
        bool                                    showDetails                                     = false;

        static Vector2                          windowsMinSize                                  = new Vector2 (1400, 720);
        const string                            EDITOR_NAME                                     = "Character Editor";

        [MenuItem("RPG/Character Editor")]
        public static void Init()
        {
            CharacterEditor window = GetWindow<CharacterEditor>();
            window.minSize = windowsMinSize;
            window.titleContent.text = EDITOR_NAME;
            window.Show();
        }

        void OnEnable()
        {
            if (_characterDatabase == null)
                _characterDatabase = CharacterDatabase.GetDatabase<CharacterDatabase>(DATABASE_PATH, DATABASE_NAME);

            _skillDatabase = Resources.Load ("Database/SkillDatabase") as SkillDatabase;

            if (_skillDatabase == null)
                Debug.LogWarning ("Skill Database not loaded");

            _skillOptions = new string[_skillDatabase.Count];

            for (int i = 0; i < _skillDatabase.Count; i++)
            {
                _skillOptions[i] = _skillDatabase.Get (i).Name;
            }

        }

        void OnGUI()
        {
            TopTabBar();

            GUILayout.BeginHorizontal();
            {                
                ListView();
                Details();
            }
            GUILayout.EndHorizontal();
        }        
    }
}