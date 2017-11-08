using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueUI;
    public Text nameText;
    public Text dialogueText;

    Queue<string> sentences;

    void Awake ()
    {
        #region Singleton

        if (instance != null)
        {
            Debug.LogWarning ("More then one DialogueManager");
            return;
        }

        instance = this;

        #endregion

        if (dialogueUI == null)
            Debug.LogWarning ("Please assign dialogueUI");

        if (nameText == null)
            Debug.LogWarning ("Please assign nameText");

        if (dialogueText == null)
            Debug.LogWarning ("Please assign dialogueText");

        // Disable dialogue UI on default
        dialogueUI.SetActive (false);
    }

    void Start ()
    {
        sentences = new Queue<string> ();
	}

    public void StartDialogue (Dialogue dialogue)
    {
        dialogueUI.SetActive (true);

        nameText.text = dialogue.name;

        // Clear the previous dialogue
        if (sentences != null)
            sentences.Clear ();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue (sentence);
        }

        DisplayNextSentence ();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue ();
            return;
        }

        string sentence = sentences.Dequeue ();
                
        StopAllCoroutines ();
        StartCoroutine (TypeSentence (sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray ())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue ()
    {
        dialogueUI.SetActive (false);

        if (GameManager.instance.gameState == GameManager.GameState.InConversationBeforeBattle)
        {
            GameManager.instance.gameState = GameManager.GameState.InBattle;
            GameManager.instance.EnterBattle ();
        }
        else
        {
            GameManager.instance.gameState = GameManager.GameState.Default;
            Debug.Log ("Enter default state");
        }
    }
}
