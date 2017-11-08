using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class ConditionTypeEditor
    {
        //Vector2 _scrollPos;

        //void ListView()
        //{
        //    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
        //    {
        //        DisplayConditionTypes();
        //    }
        //    EditorGUILayout.EndScrollView();
        //}

        //void DisplayConditionTypes()
        //{
        //    for (int i = 0; i < ConditionTypeDatabase.Count; i++)
        //    {
        //        GUILayout.BeginHorizontal("Box");
        //        {
        //            ConditionTypeDatabase.Get(i).Name = GUILayout.TextField(ConditionTypeDatabase.Get(i).Name);
        //            if (GUILayout.Button("Delete", GUILayout.Width(60)))
        //            {
        //                if (EditorUtility.DisplayDialog("Delete Condition Type", "Are you sure that you want to delete " + ConditionTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
        //                {
        //                    ConditionTypeDatabase.Remove(i);
        //                }
        //            }
        //        }
        //        GUILayout.EndHorizontal();
        //    }
        //}
    }
}