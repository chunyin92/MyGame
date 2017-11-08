using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillStatusEffectTypeEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                DisplaySkillStatusEffectTypes();
            }
            EditorGUILayout.EndScrollView();
        }

        void DisplaySkillStatusEffectTypes()
        {
            for (int i = 0; i < skillStatusEffectTypeDatabase.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                {
                    skillStatusEffectTypeDatabase.Get(i).Name = GUILayout.TextField(skillStatusEffectTypeDatabase.Get(i).Name);
                    if (GUILayout.Button("Delete", GUILayout.Width(60)))
                    {
                        if (EditorUtility.DisplayDialog("Delete Skill Status Effect Type", "Are you sure that you want to delete " + skillStatusEffectTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
                        {
                            skillStatusEffectTypeDatabase.Remove(i);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
