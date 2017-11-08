using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace RPG.BattleSystem
{
    public partial class BattleManager
    {
        int selectedSkillIndex = -1;
        int selectedTargetIndex = -1;

        void PlayerChoice()
        {
            DisplaySkill ();

            if (selectedSkillIndex != -1)
            {
                SelectTarget ();

                foreach (Button b in SkillButtons)
                {
                    b.gameObject.SetActive (false);
                }
            }
                
        }

        void DisplaySkill ()
        {
            for (int i = 0; i < Ally[fastestCharacterIndex].Skill.Count; i++)
            {
                SkillButtons[i].gameObject.SetActive (true);

                int buttonIndex;
                buttonIndex = i;

                SkillButtons[i].GetComponentInChildren<TextMeshProUGUI> ().text = Ally[fastestCharacterIndex].Skill[i].Name;
                SkillButtons[i].onClick.AddListener(() => SelectSkill (buttonIndex));

            }
            //bug.Log (selectedSkillIndex);



            //SkillButtons[0].onClick.AddListener (() => SelectSkill (0));
            //SkillButtons[1].onClick.AddListener (() => SelectSkill (1));
            //SkillButtons[2].onClick.AddListener (() => SelectSkill (2));
            //SkillButtons[3].onClick.AddListener (() => SelectSkill (3));
        }

        void SelectSkill (int index)
        {
            selectedSkillIndex = index;
        }




        //void ShowCharacterSkill()
        //{
        //    for (int i = 0; i < Ally[fastestCharacterIndex].Skill.Count; i++)
        //    {



        //        //if (GUI.Button (new Rect (Screen.width - 500 + i * 100, Screen.height - 50, 80, 30), "CD"))
        //        //{
        //        //    Debug.Log (Ally[fastestCharacterIndex].CharacterData.BaseSkill[selectedSkillIndex].Name + " is on CD");
        //        //}

        //        if (GUI.Button (new Rect (Screen.width - 500 + i * 100, Screen.height - 50, 80, 30), Ally[fastestCharacterIndex].Skill[i].Name))
        //        {
        //            // TODO make cool down work
        //            //if (Ally[fastestCharacterIndex].CharacterData.BaseSkill[i].CooldownCounter > 0)
        //            //    Debug.Log (Ally[fastestCharacterIndex].CharacterData.BaseSkill[i].Name + " is on CD");

        //            //if (Ally[fastestCharacterIndex].CharacterData.BaseSkill[i].CooldownCounter == 0)
        //            {
        //                selectedSkillIndex = i;
        //                Debug.Log ("Skill selected: " + Ally[fastestCharacterIndex].Skill[selectedSkillIndex].Name);
        //            }
        //        }
        //    }
        //}

        void SelectTarget()
        {
            //Debug.Log ("AAA" + selectedSkillIndex);

            if (Input.GetMouseButtonDown(0))
            {
                // create a ray going into the scene from the screen location the user clicked at
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // the raycast hit info will be filled by the Physics.Raycast() call further
                RaycastHit hit;

                // perform a raycast using our new ray. 
                // If the ray collides with something solid in the scene, the "hit" structure will
                // be filled with collision information
                if (Physics.Raycast(ray, out hit))
                {
                    // a collision occured. Check if it's our plane object and create our cube at the
                    // collision point, facing toward the collision normal

                    for (int i = 0; i < Enemy.Count; i++)
                    {
                        if (hit.collider.gameObject == Enemy[i].Prefab)
                        {
                            selectedTargetIndex = i;
                            if (!Enemy[selectedTargetIndex].IsDead)
                            {
                                //Ally[fastestCharacterIndex].Skill[selectedSkillIndex].CooldownCounter += Ally[fastestCharacterIndex].Skill[selectedSkillIndex].Cooldown;

                                //Debug.Log("cd counter: " + Ally[fastestCharacterIndex].Skill[selectedSkillIndex].CooldownCounter);

                                Ally[fastestCharacterIndex].IsMoved = true;
                                currentBattleState = BattleState.PROCESS_ACTION;


                                Debug.Log (Ally[fastestCharacterIndex].Name);
                                Debug.Log (Enemy[selectedTargetIndex].Name);
                                Debug.Log (selectedSkillIndex);
                                Debug.Log (Ally[fastestCharacterIndex].Skill[selectedSkillIndex].Name);

                              Debug.Log(Ally[fastestCharacterIndex].Name + " Attacked " + Enemy[selectedTargetIndex].Name + " with skill: " + Ally[fastestCharacterIndex].Skill[selectedSkillIndex].Name);
                            }
                        }
                    }

                    //if (hit.collider.transform.tag == "Enemy")
                    //{
                    //    selectedTargetIndex = int.Parse(hit.collider.transform.name);

                    //    if (!Enemy[selectedTargetIndex]._isDead)
                    //    {
                    //        Ally[fastestCharacterIndex]._isMoved = true;
                    //        currentBattleState = BattleState.CALCULATE_DAMAGE;

                    //        Debug.Log(Ally[fastestCharacterIndex]._name + " Attacked " + Enemy[selectedTargetIndex]._name);
                    //    }                        
                    //}
                }     
            }
        }


    }
}
