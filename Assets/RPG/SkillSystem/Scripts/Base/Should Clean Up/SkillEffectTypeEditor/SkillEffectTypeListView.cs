using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillEffectTypeEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                //DisplaySkillEffectTypes();
            }
            EditorGUILayout.EndScrollView();
        }

        //void DisplaySkillEffectTypes()
        //{
        //    for (int i = 0; i < skillEffectTypeDatabase.Count; i++)
        //    {
        //        GUILayout.BeginHorizontal("Box");
        //        {
        //            skillEffectTypeDatabase.Get(i).Name = GUILayout.TextField(skillEffectTypeDatabase.Get(i).Name);
        //            if (GUILayout.Button("Delete", GUILayout.Width(60)))
        //            {
        //                if (EditorUtility.DisplayDialog("Delete Skill Effect Type", "Are you sure that you want to delete " + skillEffectTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
        //                {
        //                    skillEffectTypeDatabase.Remove(i);
        //                }
        //            }
        //        }
        //        GUILayout.EndHorizontal();
        //    }
        //}
    }
}
