using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIManager : MonoBehaviour
{
    #region Singleton
    private static WorldUIManager _instance;

    private WorldUIManager ()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static WorldUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                new WorldUIManager ();
            }

            return _instance;
        }
    }
    #endregion

    public Transform worldCanvas;
    public GameObject warningIcon;
    public GameObject questionIcon;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void CreatePopupIcon (GameObject owner, GameObject icon)
    {
        GameObject go = TrashMan.spawn (icon);
        go.GetComponent<FloatingIcon> ().TargetTransform = owner.transform;
        go.transform.SetParent (worldCanvas.transform, false);
    }

    public void CreateWarningIcon (GameObject owner)
    {
        CreatePopupIcon (owner, warningIcon);
    }

    public void CreateQuestionIcon (GameObject owner)
    {
        CreatePopupIcon (owner, questionIcon);
    }
}
