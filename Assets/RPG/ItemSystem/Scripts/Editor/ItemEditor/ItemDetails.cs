using UnityEngine;
using UnityEditor;

namespace RPG.ItemSystem.Editor
{
    public partial class ItemEditor
    {
        bool showDetails = false;

        void Details()
        {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            {
                GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                {
                    if (showDetails)
                        DisplayDetails();
                }
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                {
                    DisplayButton();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        void DisplayDetails()
        {
            temp.OnGUI();
        }

        void DisplayButton()
        {
            if (showDetails)
            {
                ButtonSave();

                if (_selectedIndex > -1)
                    ButtonDelete();

                ButtonCancel();
            }
            else
            {
                ButtonCreate();
            }
        }

        void ButtonCreate()
        {
            if (GUILayout.Button("Create"))
            {
                temp = new Weapon();
                showDetails = true;
            }
        }

        void ButtonSave()
        {
            if (GUILayout.Button("Save"))
            {
                if (_selectedIndex == -1)
                    database.Add(temp);
                else
                    database.Replace(_selectedIndex, temp);

                temp = null;
                showDetails = false;
                _selectedIndex = -1;
                GUI.FocusControl(null);
            }
        }

        void ButtonCancel()
        {
            if (GUILayout.Button("Cancel"))
            {
                temp = null;
                showDetails = false;
                _selectedIndex = -1;
                GUI.FocusControl(null);
            }
        }

        void ButtonDelete()
        {
            if (GUILayout.Button("Delete"))
            {
                if (EditorUtility.DisplayDialog("Delete " + temp.Name, "Are you sure that you want to delete " + temp.Name + " from the database?", "Delete", "Cancel"))
                {
                    database.Remove(_selectedIndex);

                    temp = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl(null);
                }
            }
        }
    }	
}
