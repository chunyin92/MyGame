using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillTargetTypeEditor : EditorWindow
    {
        SkillTargetTypeDatabase skillTargetTypeDatabase;

        const string DATABASE_NAME = @"SkillTargetTypeDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        public static void Init()
        {
            SkillTargetTypeEditor window = GetWindow<SkillTargetTypeEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Skill Target Type Database";
            window.Show();
        }

        void OnEnable()
        {
            if (skillTargetTypeDatabase == null)
                skillTargetTypeDatabase = SkillTargetTypeDatabase.GetDatabase<SkillTargetTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (skillTargetTypeDatabase == null)
            {
                Debug.LogWarning("Skill Target Type Database not loaded");
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
            GUILayout.Label("Skill Target Types: " + skillTargetTypeDatabase.Count);

            if (GUILayout.Button("Add"))
            {
                skillTargetTypeDatabase.Add(new SkillTargetType());
            }
        }
    }
}
