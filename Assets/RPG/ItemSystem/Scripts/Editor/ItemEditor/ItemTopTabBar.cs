using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
    public partial class ItemEditor
    {
        void TopTabBar()
        {
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            {
                GUILayout.Button("Weapon");
                GUILayout.Button("Armor");
                GUILayout.Button("Potion");
                GUILayout.Button("About");
                GUILayout.EndHorizontal();
            }           
        }
    }
}
