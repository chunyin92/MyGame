using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorUI : MonoBehaviour
{
    AIController myAIController;
    public Animator myAnimator;

	void Start ()
    {
        myAIController = GetComponent<AIController> ();
        myAIController.OnSeeCharacter += DisplayWarningIcon;
        myAIController.OnMissCharacter += DisplayMissingIcon;
    }
	
	void Update ()
    {
		
	}

    void DisplayWarningIcon ()
    {
        myAnimator.SetTrigger ("SeeTarget");
    }

    void DisplayMissingIcon ()
    {
        //myAnimator.SetTrigger ("MissTarget");
    }

    void OnDestroy ()
    {
        Debug.Log ("Destroy");
        myAIController.OnSeeCharacter -= DisplayWarningIcon;
        myAIController.OnMissCharacter -= DisplayMissingIcon;
    }
}
