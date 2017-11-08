using UnityEngine;
using UnityEditor;
using RPG.SkillSystem;

namespace RPG.CharacterSystem.Editor
{
    public partial class CharacterEditor
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
                _tempCharacterData.Name = EditorGUILayout.TextField ("Name: ", _tempCharacterData.Name);
                _tempCharacterData.Icon = EditorGUILayout.ObjectField ("Icon", _tempCharacterData.Icon, typeof (Sprite), false) as Sprite;
                _tempCharacterData.Prefab = EditorGUILayout.ObjectField ("Prefab", _tempCharacterData.Prefab, typeof (GameObject), false) as GameObject;
                _tempCharacterData.Description = EditorGUILayout.TextField ("Description: ", _tempCharacterData.Description);
                _tempCharacterData.BaseHP = EditorGUILayout.IntSlider ("BaseHp", _tempCharacterData.BaseHP, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseMP = EditorGUILayout.IntSlider ("BaseMp", _tempCharacterData.BaseMP, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseAttack = EditorGUILayout.IntSlider ("BaseAttack", _tempCharacterData.BaseAttack, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseDefense = EditorGUILayout.IntSlider ("BaseDefense", _tempCharacterData.BaseDefense, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseTalent = EditorGUILayout.IntSlider ("BaseTalent", _tempCharacterData.BaseTalent, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseSpeed = EditorGUILayout.IntSlider ("BaseSpeed", _tempCharacterData.BaseSpeed, 0, 150, GUILayout.Width (300));
                _tempCharacterData.BaseCriticalRate = EditorGUILayout.IntSlider ("BaseCriticalRate", _tempCharacterData.BaseCriticalRate, 0, 100, GUILayout.Width (300));
                _tempCharacterData.BaseCriticalDamage = EditorGUILayout.IntSlider ("BaseCriticalDamage", _tempCharacterData.BaseCriticalDamage, 0, 100, GUILayout.Width (300));
                DisplaySkill ();
            }
            GUILayout.EndVertical ();
        }

        public void DisplaySkill ()
        {
            GUILayout.Space (20);
            GUILayout.Label ("Skill");

            #region Button for add/delete skill
            GUILayout.BeginHorizontal ();
            {
                if (GUILayout.Button ("+", GUILayout.Width (20)))
                    _tempCharacterData.SkillData.Add (new SkillData ());

                if (GUILayout.Button ("-", GUILayout.Width (20)))
                {
                    if (_tempCharacterData.SkillData.Count == 0)
                        return;

                    _tempCharacterData.SkillData.RemoveAt (_tempCharacterData.SkillData.Count - 1);
                }
            }
            GUILayout.EndHorizontal ();
            #endregion

            if (_tempCharacterData.SkillData == null)
            {
                Debug.LogWarning ("_tempCharacter.BaseSkill == Null");
                return;
            }

            #region Display skill property labels
            if (_tempCharacterData.SkillData.Count > 0)
            {
                GUILayout.BeginHorizontal ();
                {
                    GUILayout.Space (150);
                    GUILayout.Label ("Name", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Base Power", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Cooldown", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Mana Cost", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Accuracy", GUILayout.Width (_labelWidth));
                    GUILayout.Label ("Target Type", GUILayout.Width (_labelWidth));
                }
                GUILayout.EndHorizontal ();
            }
            #endregion

            #region Edit skill properties
            for (int i = 0; i < _tempCharacterData.SkillData.Count; i++)
            {
                int skillIndex = 0;

                if (_tempCharacterData.SkillData[i] != null)
                    skillIndex = _skillDatabase.GetIndex (_tempCharacterData.SkillData[i].Name);

                if (skillIndex == -1)
                    skillIndex = 0;

                GUILayout.BeginHorizontal ();
                {
                    GUILayout.Space (150);
                    _tempCharacterData.SkillData[i] = _skillDatabase.Get (EditorGUILayout.Popup ("", skillIndex, _skillOptions, GUILayout.Width (_labelWidth)));

                    GUILayout.Label (_tempCharacterData.SkillData[i].BasePower.ToString (), GUILayout.Width (_labelWidth));
                    GUILayout.Label (_tempCharacterData.SkillData[i].Cooldown.ToString (), GUILayout.Width (_labelWidth));
                    GUILayout.Label (_tempCharacterData.SkillData[i].ManaCost.ToString (), GUILayout.Width (_labelWidth));
                    GUILayout.Label (_tempCharacterData.SkillData[i].Accuracy.ToString (), GUILayout.Width (_labelWidth));
                    GUILayout.Label (_tempCharacterData.SkillData[i].TargetType.ToString (), GUILayout.Width (_labelWidth));
                }
                GUILayout.EndHorizontal ();

                GUILayout.BeginHorizontal ();
                {
                    GUILayout.Label ("Description: ", GUILayout.Width (150));
                    GUILayout.Label (_tempCharacterData.SkillData[i].Description);
                }
                GUILayout.EndHorizontal ();
                #endregion
            }
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
                _tempCharacterData = new CharacterData();
                showDetails = true;
            }
        }

        void ButtonSave()
        {
            if (GUILayout.Button("Save"))
            {
                if (_selectedIndex == -1)
                    _characterDatabase.Add(_tempCharacterData);
                else
                    _characterDatabase.Replace(_selectedIndex, _tempCharacterData);

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
                if (EditorUtility.DisplayDialog("Delete " + _tempCharacterData.Name, "Are you sure that you want to delete " + _tempCharacterData.Name + " from the database?", "Delete", "Cancel"))
                {
                    _characterDatabase.Remove(_selectedIndex);
                    ExitEditMode ();
                }
            }
        }

        void ExitEditMode ()
        {
            _tempCharacterData = null;
            showDetails = false;
            _selectedIndex = -1;
            GUI.FocusControl (null);
        }
    }
}
