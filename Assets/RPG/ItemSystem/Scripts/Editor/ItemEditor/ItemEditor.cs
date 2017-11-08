using UnityEngine;
using UnityEditor;

namespace RPG.ItemSystem.Editor
{
    public partial class ItemEditor : EditorWindow
    {
        WeaponDatabase database;

        const string DATABASE_NAME = @"WeaponDatabase.asset";
        const string DATABASE_PATH = @"Resources/Database";

        [MenuItem("RPG/ItemSystem/Item Editor")]
        public static void Init()
        {
            ItemEditor window = GetWindow<ItemEditor>();
            window.minSize = new Vector2(1280, 720);
            window.titleContent.text = "Item Editor";
            window.Show();
        }

        void OnEnable()
        {
            if (database == null)
                database = WeaponDatabase.GetDatabase<WeaponDatabase>(DATABASE_PATH, DATABASE_NAME);
        }

        void OnGUI()
        {
            TopTabBar();

            GUILayout.BeginHorizontal();
            {
                ListView();
                Details();
            }
            GUILayout.EndHorizontal();

            BottomBar();
        }
    }
}
