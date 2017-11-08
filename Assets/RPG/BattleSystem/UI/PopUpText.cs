using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public Animator myAnimator;
    private TextMeshProUGUI mytext;

	void OnEnable ()
    {
        AnimatorClipInfo[] clipInfo = myAnimator.GetCurrentAnimatorClipInfo (0);
        mytext = myAnimator.GetComponent<TextMeshProUGUI> ();
        TrashMan.despawnAfterDelay (gameObject, clipInfo[0].clip.length);
	}
	
    public void DisplayText(string text, bool isDamage)
    {

        if (isDamage)
            mytext.color = Color.red;
        else
            mytext.color = Color.green;
        //mytext.fontSize = 50;
        mytext.text = text;
    }
}
