using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
    public partial class ItemEditor
    {
        Vector2 _scrollPos = Vector2.zero;

        int _listViewWidth = 200;
        int _listViewButtonWidth = 190;
        int _listViewButtonHieght = 25;

        int _selectedIndex = -1;

        Weapon temp = new Weapon();

        void ListView()
        {
            if (showDetails)
                GUI.enabled = false;

            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(_listViewWidth));
            {
                for (int i = 0; i < database.Count; i++)
                {
                    if (GUILayout.Button(database.Get(i).Name, "Box", GUILayout.Width(_listViewButtonWidth), GUILayout.Height(_listViewButtonHieght)))
                    {
                        temp = new Weapon();
                        temp.Clone(database.Get(i));
                        showDetails = true;
                        _selectedIndex = i;
                    }
                }
            }
            GUILayout.EndScrollView();

            GUI.enabled = true;
        }
    }
}