using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class ConditionTypeEditor : EditorWindow
    {
        //ConditionTypeDatabase ConditionTypeDatabase;

        //const string DATABASE_NAME = @"ConditionTypeDatabase.asset";
        //const string DATABASE_PATH = @"Resources/Database";
        
        //public static void Init()
        //{
        //    ConditionTypeEditor window = GetWindow<ConditionTypeEditor>();
        //    window.minSize = new Vector2(400, 300);
        //    window.titleContent.text = "Condition Type Database";
        //    window.Show();
        //}

        //void OnEnable()
        //{
        //    if (ConditionTypeDatabase == null)
        //        ConditionTypeDatabase = ConditionTypeDatabase.GetDatabase<ConditionTypeDatabase>(DATABASE_PATH, DATABASE_NAME);
        //}

        //void OnGUI()
        //{
        //    if (ConditionTypeDatabase == null)
        //    {
        //        Debug.LogWarning("Condition Type Database not loaded");
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
        //    GUILayout.Label("Condition Types: " + ConditionTypeDatabase.Count);

        //    if (GUILayout.Button("Add"))
        //    {
        //        ConditionTypeDatabase.Add(new ConditionType());
        //    }
        //}
    }
}