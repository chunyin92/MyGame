using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillStatusEffectTypeEditor : EditorWindow
    {
        SkillStatusEffectTypeDatabase skillStatusEffectTypeDatabase;

        const string DATABASE_NAME = @"SkillStatusEffectTypeDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        public static void Init()
        {
            SkillStatusEffectTypeEditor window = GetWindow<SkillStatusEffectTypeEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Skill Status Effect Type Database";
            window.Show();
        }

        void OnEnable()
        {
            if (skillStatusEffectTypeDatabase == null)
                skillStatusEffectTypeDatabase = SkillStatusEffectTypeDatabase.GetDatabase<SkillStatusEffectTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (skillStatusEffectTypeDatabase == null)
            {
                Debug.LogWarning("Skill Status Effect Type Database not loaded");
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
            GUILayout.Label("Skill Status Effect Types: " + skillStatusEffectTypeDatabase.Count);

            if (GUILayout.Button("Add"))
            {
                skillStatusEffectTypeDatabase.Add(new SkillStatusEffectType());
            }
        }
    }
}
