using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.BattleSystem;
using RPG.CharacterSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Interactable
{
    public List<string> EnemyName;
    public List<int> EnemyLevel;

    GameManager GM;
    BattleController BC;
    CharacterDatabase characterdb;

    public override void Interact ()
    {
        GM = GameManager.instance;
        BC = BattleController.instance;
        characterdb = Resources.Load ("Database/CharacterDatabase") as CharacterDatabase;

        if (characterdb == null)
            Debug.LogWarning ("Character Database not loaded");


        base.Interact ();

        if (transform.GetComponent<DialogueTrigger> () != null)
        {
            GM.gameState = GameManager.GameState.InConversationBeforeBattle;
            transform.GetComponent<DialogueTrigger> ().TriggerDialogue ();
        }
        else
        {
            GM.gameState = GameManager.GameState.InBattle;
            
            for (int i = 0; i < EnemyName.Count; i++)
            {
                BC.Enemy.Add (new Character (characterdb.Get (EnemyName[i]), EnemyLevel[i]));
            }
            
            StartBattle ();            
        }
    }

    void StartBattle ()
    {
        GM.EnterBattle ();
        Destroy (gameObject);
    }
}