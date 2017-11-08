using UnityEngine;
using UnityEditor;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillPassiveEffectTypeEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                DisplaySkillPassiveEffectTypes();
            }
            EditorGUILayout.EndScrollView();
        }

        void DisplaySkillPassiveEffectTypes()
        {
            for (int i = 0; i < skillPassiveEffectTypeDatabase.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                {
                    skillPassiveEffectTypeDatabase.Get(i).Name = GUILayout.TextField(skillPassiveEffectTypeDatabase.Get(i).Name);
                    if (GUILayout.Button("Delete", GUILayout.Width(60)))
                    {
                        if (EditorUtility.DisplayDialog("Delete Skill Passive Effect Type", "Are you sure that you want to delete " + skillPassiveEffectTypeDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
                        {
                            skillPassiveEffectTypeDatabase.Remove(i);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}