using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillTypeEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                DisplaySkillTypes();
            }
            EditorGUILayout.EndScrollView();
        }

        void DisplaySkillTypes()
        {
            for (int i = 0; i < skillTypeDatabase.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                {
                    skillTypeDatabase.Get(i).Name = GUILayout.TextField(skillTypeDatabase.Get(i).Name);
                    if (GUILayout.Button("Delete" , GUILayout.Width(60)))
                    {
                        if (EditorUtility.DisplayDialog("Delete Skill Type", "Are you sure that you want to delete " + skillTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
                        {
                            skillTypeDatabase.Remove(i);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
