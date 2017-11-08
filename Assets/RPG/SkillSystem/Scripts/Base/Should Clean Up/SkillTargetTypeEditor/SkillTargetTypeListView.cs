using UnityEngine;
using System.Collections;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillTargetTypeEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                DisplaySkillTargetTypes();
            }
            EditorGUILayout.EndScrollView();
        }

        void DisplaySkillTargetTypes()
        {
            for (int i = 0; i < skillTargetTypeDatabase.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                {
                    skillTargetTypeDatabase.Get(i).Name = GUILayout.TextField(skillTargetTypeDatabase.Get(i).Name);
                    if (GUILayout.Button("Delete" , GUILayout.Width(60)))
                    {
                        if (EditorUtility.DisplayDialog("Delete Skill Target Type", "Are you sure that you want to delete " + skillTargetTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
                        {
                            skillTargetTypeDatabase.Remove(i);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
