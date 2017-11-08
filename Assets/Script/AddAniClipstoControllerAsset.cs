using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AddAniClipstoControllerAsset : MonoBehaviour
{
    Object anim;
    public AnimationClip animationClipToBeAdded;

    void Start ()
    {
        anim = AssetDatabase.LoadAssetAtPath ("Assets/Animations/UI/Button.controller", (typeof (Object)));
    }
	
	void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (anim)
            {
                AnimationClip animationClip = new AnimationClip ();
                //animationClip = animationClipToBeAdded;

                //animationClip = (AnimationClip)AssetDatabase.LoadAssetAtPath ("Assets/Animations/UI/Button_test.anim", (typeof (AnimationClip)));
                animationClip.name = "Test";
                AssetDatabase.AddObjectToAsset (animationClip, anim);
                AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath (animationClip));
                Debug.Log ("ADded");
            }            
        }
	}
}
