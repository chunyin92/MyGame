using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillPassiveEffectTypeEditor : EditorWindow
    {
        SkillPassiveEffectTypeDatabase skillPassiveEffectTypeDatabase;

        const string DATABASE_NAME = @"SkillPassiveEffectTypeDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        public static void Init()
        {
            SkillPassiveEffectTypeEditor window = GetWindow<SkillPassiveEffectTypeEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Skill Passive Effect Type Database";
            window.Show();
        }

        void OnEnable()
        {
            if (skillPassiveEffectTypeDatabase == null)
                skillPassiveEffectTypeDatabase = SkillPassiveEffectTypeDatabase.GetDatabase<SkillPassiveEffectTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (skillPassiveEffectTypeDatabase == null)
            {
                Debug.LogWarning("Skill Passive Effect Type Database not loaded");
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
            GUILayout.Label("Skill Passive Effect Types: " + skillPassiveEffectTypeDatabase.Count);

            if (GUILayout.Button("Add"))
            {
                skillPassiveEffectTypeDatabase.Add(new SkillPassiveEffectType());
            }
        }
    }
}