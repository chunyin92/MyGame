using UnityEngine;

namespace RPG.CharacterSystem.Editor
{
    public partial class CharacterEditor
    {
        void ListView()
        {
            if (showDetails)
                GUI.enabled = false;

            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(_listViewWidth));
            {
                for (int i = 0; i < _characterDatabase.Count; i++)
                {
                    GUILayout.BeginHorizontal ();
                    {
                        if (GUILayout.Button ("↑", GUILayout.Width (20), GUILayout.Height (20)))
                            _characterDatabase.MoveUp (i);

                        if (GUILayout.Button(_characterDatabase.Get(i).Name, "Box", GUILayout.Width(_listViewButtonWidth), GUILayout.Height(_listViewButtonHieght)))
                        {
                            _tempCharacterData = new CharacterData (_characterDatabase.Get (i));

                            //_tempCharacterData = new CharacterData();
                            //_tempCharacterData.Clone(_characterDatabase.Get(i));

                            if (_tempCharacterData.SkillData == null)
                                Debug.Log ("NULL");
                            
                            showDetails = true;
                            _selectedIndex = i;
                        }

                        if (GUILayout.Button ("↓", GUILayout.Width (20), GUILayout.Height (20)))
                            _characterDatabase.MoveDown (i);
                    }
                    GUILayout.EndHorizontal ();
                }
            }
            GUILayout.EndScrollView();

            GUI.enabled = true;
        }        
    }
}