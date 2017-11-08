using UnityEngine;
using UnityEditor;
using RPG.CharacterSystem;

namespace RPG.SkillSystem.Editor
{
    public partial class SkillEditor
    {
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
            GUILayout.BeginVertical ();
            {
                if (_selectedIndex == -1)
                    _tempSkillData.Name = EditorGUILayout.TextField ("Name", _tempSkillData.Name);
                else
                {
                    GUILayout.Label ("Name                             " + _tempSkillData.Name);
                }
                _tempSkillData.Icon = EditorGUILayout.ObjectField ("Icon", _tempSkillData.Icon, typeof (Sprite), false) as Sprite;
                _tempSkillData.Description = EditorGUILayout.TextField ("Description", _tempSkillData.Description);
                _tempSkillData.BasePower = EditorGUILayout.IntSlider ("BasePower", _tempSkillData.BasePower, 0, 150, GUILayout.Width (300));
                _tempSkillData.Cooldown = EditorGUILayout.IntSlider ("Cooldown", _tempSkillData.Cooldown, 0, 10, GUILayout.Width (300));
                _tempSkillData.ManaCost = EditorGUILayout.IntSlider ("Mana Cost", _tempSkillData.ManaCost, 0, 100, GUILayout.Width (300));
                _tempSkillData.Accuracy = EditorGUILayout.IntSlider ("Accuracy", _tempSkillData.Accuracy, 0, 100, GUILayout.Width (300));
                _tempSkillData.TargetType = (TargetTypes)EditorGUILayout.EnumPopup ("Target Type", _tempSkillData.TargetType, GUILayout.Width (300));
                DisplaySkillEffect ();

                //_tempSkill.BasePower = SetValueToBeMultipleOfSomeNumber (_tempSkill.BasePower, 5);
                //_tempSkill.ManaCost = SetValueToBeMultipleOfSomeNumber (_tempSkill.ManaCost, 5);
                //_tempSkill.Accuracy = SetValueToBeMultipleOfSomeNumber (_tempSkill.Accuracy, 5);
            }
            GUILayout.EndVertical ();
        }

        public void DisplaySkillEffect ()
        {
            GUILayout.Space (20);
            GUILayout.Label ("Effect");            

            #region Button for add/delete effect
            GUILayout.BeginHorizontal ();
            {
                if (GUILayout.Button ("+", GUILayout.Width (20)))
                    _tempSkillData.Effect.Add (new Effect ());

                if (GUILayout.Button ("-", GUILayout.Width (20)))
                {
                    // If there is no effect, i.e. 0 effect, do not remove
                    if (_tempSkillData.Effect.Count == 0)
                        return;

                    // Remove the last effect
                    _tempSkillData.Effect.RemoveAt (_tempSkillData.Effect.Count - 1);
                }
            }
            GUILayout.EndHorizontal ();
            #endregion

            if (_tempSkillData.Effect == null)
            {
                Debug.LogWarning ("_tempSkill.Effect == Null");
                return;
            }

            #region Display effect property labels
            if (_tempSkillData.Effect.Count > 0)
            {
                GUILayout.BeginHorizontal ();
                {
                    GUILayout.Space (150);
                    GUILayout.Label ("Condition type", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Probability", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Target type", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Effect type", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Percentage", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Duration", GUILayout.Width (_labelWidth));
                }
                GUILayout.EndHorizontal ();
            }
            #endregion

            #region Edit effect properties
            for (int i = 0; i < _tempSkillData.Effect.Count; i++)
            {
                GUILayout.BeginHorizontal ();
                {
                    GUILayout.Space (150);

                    _tempSkillData.Effect[i].ConditionType = (ConditionTypes)EditorGUILayout.EnumPopup ("", _tempSkillData.Effect[i].ConditionType, GUILayout.Width (_labelWidth));
                    _tempSkillData.Effect[i].Probability = EditorGUILayout.IntSlider (_tempSkillData.Effect[i].Probability, 0, 100, GUILayout.Width (_labelWidth));
                    _tempSkillData.Effect[i].TargetType = (TargetTypes)EditorGUILayout.EnumPopup ("", _tempSkillData.Effect[i].TargetType, GUILayout.Width (_labelWidth));
                    _tempSkillData.Effect[i].EffectType = (EffectTypes)EditorGUILayout.EnumPopup ("", _tempSkillData.Effect[i].EffectType, GUILayout.Width (_labelWidth));
                    _tempSkillData.Effect[i].Percentage = EditorGUILayout.IntSlider (_tempSkillData.Effect[i].Percentage, -100, 100, GUILayout.Width (_labelWidth));
                    _tempSkillData.Effect[i].Duration = EditorGUILayout.IntSlider (_tempSkillData.Effect[i].Duration, 1, 10, GUILayout.Width (_labelWidth));
                    //_tempSkill.Effect[i].Probability = SetValueToBeMultipleOfSomeNumber (_tempSkill.Effect[i].Probability, 5);
                    //_tempSkill.Effect[i].Percentage = SetValueToBeMultipleOfSomeNumber (_tempSkill.Effect[i].Percentage, 5);
                }
                GUILayout.EndHorizontal ();
            }
            #endregion
        }
        
        int SetValueToBeMultipleOfSomeNumber (int value, int num)
        {
            if (value % num != 0)
                value = num * (int)(value / (float)num);
            return value;
        }

        void DisplayButton ()
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
                // we do not have to modify _selectedIndex since we will just add the new skill to the end of the database (_selectedIndex == -1) 
                _tempSkillData = new SkillData();
                showDetails = true;
            }
        }

        void ButtonSave()
        {
            if (GUILayout.Button("Save"))
            {
                if (_selectedIndex == -1)
                    _skillDatabase.Add(_tempSkillData);
                else
                {
                    // TODO: need to rewrite later
                    // check if character in character system has registered the skill or not
                    
                    int count = 0;

                    for (int i = 0; i < _characterDatabase.Count; i++)
                    {
                        for (int j = 0; j < _characterDatabase.Get (i).SkillData.Count; j++)
                        {
                            if (_characterDatabase.Get (i).SkillData[j].Name == _skillDatabase.Get (_selectedIndex).Name)
                            {
                                count++;
                                Debug.Log (_characterDatabase.Get (i).Name + " is using [" + _characterDatabase.Get (i).SkillData[j].Name + "] , updated skill info.");
                                _characterDatabase.Get (i).SkillData[j] = _tempSkillData;
                            }

                        }
                    }                   

                    Debug.Log ("Total skills updated: " + count);
                    _skillDatabase.Replace (_selectedIndex, _tempSkillData);                    
                }
                ExitEditMode ();
            }
        }

        void ButtonCancel()
        {
            if (GUILayout.Button("Cancel"))
                ExitEditMode ();
        }

        void ButtonDelete()
        {
            if (GUILayout.Button("Delete"))
            {
                // need to check if character in character system has registered the skill or not, if yes, cannot delete

                int count = 0;

                for (int i = 0; i < _characterDatabase.Count; i++)
                {
                    for (int j = 0; j < _characterDatabase.Get (i).SkillData.Count; j++)
                    {
                        if (_characterDatabase.Get (i).SkillData[j].Name == _skillDatabase.Get (_selectedIndex).Name)
                        {
                            count++;
                            Debug.Log (_characterDatabase.Get (i).Name + " is using [" + _tempSkillData.Name + "].");
                        }
                    }
                }
                
                if (count > 0)
                    EditorUtility.DisplayDialog ("Error", "Cannot delete [" + _tempSkillData.Name + "]. There are " + count + " character using this skill", "OK");
                else if (EditorUtility.DisplayDialog("Delete " + _tempSkillData.Name, "Are you sure that you want to delete " + _tempSkillData.Name + " from the database?", "Delete", "Cancel"))
                {
                    _skillDatabase.Remove (_selectedIndex);

                    ExitEditMode ();
                }
            }
        }
                
        void ExitEditMode ()
        {
            // Emtpy _tempSkill, return to view list mode and update _selectedIndex
            _tempSkillData = null;
            showDetails = false;
            _selectedIndex = -1;
            GUI.FocusControl (null);
        }
    }
}
