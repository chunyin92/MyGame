using UnityEngine;
using UnityEditor;

namespace RPG.ItemSystem.Editor
{
    public partial class QualityEditor : EditorWindow
    {
        QualityDatabase QualityDatabase;

        const string DATABASE_NAME = @"QualityDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        [MenuItem("RPG/ItemSystem/Quality Editor")]
        public static void Init()
        {
            QualityEditor window = GetWindow<QualityEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Quality Database";
            window.Show();
        }

        void OnEnable()
        {
            if (QualityDatabase == null)
                QualityDatabase = QualityDatabase.GetDatabase<QualityDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            if (QualityDatabase == null)
            {
                Debug.LogWarning("Quality Database not loaded");
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
            GUILayout.Label("Qualities: " + QualityDatabase.Count);

            if (GUILayout.Button("Add"))
            {
                QualityDatabase.Add(new Quality());
            }
        }
    }
}
