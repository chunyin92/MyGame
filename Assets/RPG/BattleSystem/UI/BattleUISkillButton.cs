using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.BattleSystem;

public class BattleUISkillButton : MonoBehaviour
{
    BattleController _battleController;

    void Start ()
    {
        _battleController = BattleController.instance;
    }

    public void OnButtonClick (int index)
    {
        _battleController.selectedSkillIndex = index;
    }
}
