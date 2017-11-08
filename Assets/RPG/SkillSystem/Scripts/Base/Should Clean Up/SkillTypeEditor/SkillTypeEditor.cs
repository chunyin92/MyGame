using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillTypeEditor : EditorWindow
    {
        SkillTypeDatabase skillTypeDatabase;

        const string DATABASE_NAME = @"SkillTypeDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        public static void Init()
        {
            SkillTypeEditor window = GetWindow<SkillTypeEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Skill Type Database";
            window.Show();
        }

        void OnEnable()
        {
            if (skillTypeDatabase == null)
                skillTypeDatabase = SkillTypeDatabase.GetDatabase<SkillTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (skillTypeDatabase == null)
            {
                Debug.LogWarning("Skill Type Database not loaded");
                return;
            }

            ListView();

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            {
                BottomBar();
            }
            GUILayout.EndHorizontal();
        }

        void BottomBar()
        {
            GUILayout.Label("Skill Types: " + skillTypeDatabase.Count);

            if (GUILayout.Button("Add"))
            {
                skillTypeDatabase.Add(new SkillType());
            }
        }
    }
}
