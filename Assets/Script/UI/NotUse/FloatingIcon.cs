using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingIcon : MonoBehaviour
{
    public Animator animator;
    public Transform TargetTransform;
    public Vector3 offset = Vector3.up;

    void Start ()
    {
        //Debug.Log ("AAA");
        //transform.position = Camera.main.WorldToScreenPoint (offset + TargetTransform.position);
    }

    void LateUpdate ()
    {
        transform.position = Camera.main.WorldToScreenPoint (offset + TargetTransform.position);
    }

    void OnEnable ()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
        TrashMan.despawnAfterDelay (gameObject, clipInfo[0].clip.length);        
    }
}
