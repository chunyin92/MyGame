using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillEffectTypeEditor : EditorWindow
    {
        //EffectTypeDatabase skillEffectTypeDatabase;

        //const string DATABASE_NAME = @"SkillEffectTypeDatabase.asset";
        //const string DATABASE_PATH = @"Resources/Database";
        
        //public static void Init()
        //{
        //    SkillEffectTypeEditor window = GetWindow<SkillEffectTypeEditor>();
        //    window.minSize = new Vector2(400, 300);
        //    window.titleContent.text = "Skill Effect Type Database";
        //    window.Show();
        //}

        //void OnEnable()
        //{
        //    if (skillEffectTypeDatabase == null)
        //        skillEffectTypeDatabase = EffectTypeDatabase.GetDatabase<EffectTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        //}

        //void OnGUI()
        //{
        //    if (skillEffectTypeDatabase == null)
        //    {
        //        Debug.LogWarning("Skill Effect Type Database not loaded");
        //        return;
        //    }

        //    ListView();

        //    GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
        //    {
        //        BottomBar();
        //    }
        //    GUILayout.EndHorizontal();
        //}

        //void BottomBar()
        //{
        //    GUILayout.Label("Skill Effect Types: " + skillEffectTypeDatabase.Count);

        //    if (GUILayout.Button("Add"))
        //    {
        //        skillEffectTypeDatabase.Add(new EffectTypes());
        //    }
        //}
    }
}
