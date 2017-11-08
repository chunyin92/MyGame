using UnityEngine;
using UnityEditor;
using RPG.CharacterSystem;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillEditor : EditorWindow
    {
        SkillData                       _tempSkillData                              = new SkillData();
        SkillDatabase                   _skillDatabase;
        CharacterDatabase               _characterDatabase;

        const string                    DATABASE_PATH                           = @"Resources/Database";
        const string                    SKILL_DATABASE_NAME                     = @"SkillDatabase.asset";        

        Vector2                         _scrollPosition                         = Vector2.zero;
        int                             _listViewWidth                          = 250;
        int                             _listViewButtonWidth                    = 190;
        int                             _listViewButtonHieght                   = 25;
        int                             _labelWidth                             = 150;
        int                             _selectedIndex                          = -1;        
        bool                            showDetails                             = false;

        static Vector2                  windowsMinSize                          = new Vector2 (1400, 720);
        const string                    EDITOR_NAME                             = "Skill Editor";        

        [MenuItem("RPG/Skill Editor %#s")]
        public static void Init()
        {
            SkillEditor window = GetWindow<SkillEditor>();
            window.minSize = windowsMinSize;
            window.titleContent.text = EDITOR_NAME;
            window.Show();
        }

        void OnEnable()
        {
            LoadDataBase ();
        }

        void OnGUI()
        {
            //TopTabBar();

            GUILayout.BeginHorizontal();
            {
                ListView();
                Details();
            }
            GUILayout.EndHorizontal();

            //BottomBar();
        }

        void LoadDataBase ()
        {
            if (!_skillDatabase)
                _skillDatabase = SkillDatabase.GetDatabase<SkillDatabase> (DATABASE_PATH, SKILL_DATABASE_NAME);

            _characterDatabase = Resources.Load ("Database/CharacterDatabase") as CharacterDatabase;
            if (!_characterDatabase)
                Debug.Log ("Cannot find character database");

        }
    }
}
