using UnityEngine;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillEditor
    {
        void ListView()
        {
            if (showDetails)
                GUI.enabled = false;

            _scrollPosition = GUILayout.BeginScrollView (_scrollPosition, "Box", GUILayout.ExpandHeight (true), GUILayout.Width (_listViewWidth));
            {
                for (int i = 0; i < _skillDatabase.Count; i++)
                {
                    // make moveup, skill list and movedown button horizontal
                    GUILayout.BeginHorizontal ();
                    {
                        if (GUILayout.Button ("↑", GUILayout.Width (20), GUILayout.Height (20)))
                            _skillDatabase.MoveUp (i);

                        if (GUILayout.Button (_skillDatabase.Get (i).Name, "Box", GUILayout.Width (_listViewButtonWidth), GUILayout.Height (_listViewButtonHieght)))
                        {
                            _tempSkillData = new SkillData (_skillDatabase.Get (i));

                            //_tempSkillData = new SkillData ();
                            //_tempSkillData.Clone (_skillDatabase.Get (i));
                            showDetails = true;
                            _selectedIndex = i;
                        }

                        if (GUILayout.Button ("↓", GUILayout.Width (20), GUILayout.Height (20)))
                            _skillDatabase.MoveDown (i);
                    }
                    GUILayout.EndHorizontal ();
                }
            }
            GUILayout.EndScrollView ();

            GUI.enabled = true;
        }
    }
}
