using UnityEngine;
using UnityEditor;

namespace RPG.ItemSystem.Editor
{
    public partial class QualityEditor
    {
        Vector2 _scrollPos;

        void ListView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
            {
                DisplayQualities();
            }
            EditorGUILayout.EndScrollView();
        }

        void DisplayQualities()
        {
            for (int i = 0; i < QualityDatabase.Count; i++)
            {
                GUILayout.BeginVertical("Box");
                {
                    QualityDatabase.Get(i).Name = EditorGUILayout.TextField("Name", QualityDatabase.Get(i).Name);
                    QualityDatabase.Get(i).Icon = EditorGUILayout.ObjectField("Icon", QualityDatabase.Get(i).Icon, typeof(Sprite), false) as Sprite;
                    EditorUtility.SetDirty(QualityDatabase);

                    if (GUILayout.Button("Delete"))
                    {
                        if (EditorUtility.DisplayDialog("Delete " + QualityDatabase.Get(i).Name, "Are you sure that you want to delete " + QualityDatabase.Get(i).Name + " from the database?", "Delete", "Cancel"))
                        {
                            QualityDatabase.Remove(i);
                        }
                    }
                }                    
                GUILayout.EndVertical();               
            }
        }
    }
}
