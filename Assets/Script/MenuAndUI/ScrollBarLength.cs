using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ScrollBarLength : MonoBehaviour {

    public enum ScrollBarOrientation { horizontal, vertical}
    public ScrollBarOrientation orientation;

    RectTransform myRectTransform;
    VerticalLayoutGroup myVericalLayoutGroup;
    
    void Start ()
    {
        myRectTransform = GetComponent<RectTransform> ();
        SetScrollBarLenght ();
    }
	
	void Update ()
    {

    }

    public void SetScrollBarLenght ()
    {
        int childCount = gameObject.transform.childCount;
        float childLength;
        float adjustmentDueToAllChild;
        float adjustmentDueToPadding;
        float adjustmentDueToSpacing;
        float scrollBarLength;
        
        if (orientation == ScrollBarOrientation.horizontal)
        {
            HorizontalLayoutGroup myHorizontalLayoutGroup = GetComponent<HorizontalLayoutGroup> ();

            childLength = gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta.x;
            adjustmentDueToAllChild = childCount * childLength;
            adjustmentDueToPadding = myHorizontalLayoutGroup.padding.horizontal;
            adjustmentDueToSpacing = myHorizontalLayoutGroup.spacing * (childCount - 1);
            
            scrollBarLength = adjustmentDueToAllChild + adjustmentDueToPadding + adjustmentDueToSpacing;
            
            myRectTransform.sizeDelta = new Vector2 (scrollBarLength, 0);
        }
        else
        {
            VerticalLayoutGroup myVericalLayoutGroup = GetComponent<VerticalLayoutGroup> ();

            childLength = gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta.y;

            adjustmentDueToAllChild = childCount * childLength;
            adjustmentDueToPadding = myVericalLayoutGroup.padding.vertical;
            adjustmentDueToSpacing = myVericalLayoutGroup.spacing * (childCount - 1);

            scrollBarLength = adjustmentDueToAllChild + adjustmentDueToPadding + adjustmentDueToSpacing;

            myRectTransform.sizeDelta = new Vector2 (0, scrollBarLength);
        }
    }            
}
