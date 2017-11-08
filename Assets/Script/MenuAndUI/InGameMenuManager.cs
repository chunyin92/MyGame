using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    public static InGameMenuManager instance;

    [Header ("Panels")]
    public GameObject[] panels;

    public Button[] buttons;

    public GameObject[] panelIndicationLine;

    public GameObject inGameMenuUI;

    int activePanelIndex = 0;
    float panelMoveTime = 0.5f;
    bool isPanelmoving = false;

    void Awake ()
    {
        #region Singleton

        if (instance != null)
        {
            Debug.LogWarning ("More then one InGameMenuManager");
            Destroy (gameObject);
            return;
        }
        else
            instance = this;

        #endregion

        DontDestroyOnLoad (gameObject);

        //SelectPanel (activePanelIndex);

        buttons[0].GetComponent<Animator> ().SetTrigger ("Highlighted");
        buttons[0].GetComponent<Animator> ().SetBool ("IsSelected", true);
        Debug.Log (buttons[0].GetComponent<Animator> ().GetBool ("IsSelected"));

        inGameMenuUI.SetActive (false);
    }

    void Start ()
    {
        //buttons[0].GetComponent<Animator> ().SetBool ("IsSelected", true);
        //buttons[0].GetComponent<Animator> ().SetTrigger ("Highlighted");
    }


    void Update ()
    {

        if (Input.GetButtonDown ("InGameMenu"))
        {
            inGameMenuUI.SetActive (!inGameMenuUI.activeSelf);
            
        }

        //if (Input.GetKeyDown (KeyCode.A))
        //{
        //    line.GetComponent<Animator> ().SetBool("Selected", true);


        //    //buttons[0].GetComponentInChildren<Animator> ().SetBool ("Selected", true);
        //    //if (buttons[0].GetComponentInChildren<Animator> () != null)
        //    //    Debug.Log ("AAA");
        //}
        
    }

    public void OnClickTopBarButton (int selectedPanelIndex)
    {
        //play sound???
        SelectPanel (selectedPanelIndex);
        panelIndicationLine[activePanelIndex].SetActive (false);
        panelIndicationLine[selectedPanelIndex].SetActive (true);
    }







    public void SelectPanel(int selectedPanelIndex)
    {
        if (isPanelmoving || activePanelIndex == selectedPanelIndex)
            return;

        StartCoroutine (AnimatePanelMovement (selectedPanelIndex, panelMoveTime));
    }

    IEnumerator AnimatePanelMovement (int selectedPanelIndex, float time)
    {
        CanvasGroup selectedPanel_cg = panels[selectedPanelIndex].GetComponent<CanvasGroup> ();
        CanvasGroup activePanel_cg = panels[activePanelIndex].GetComponent<CanvasGroup> ();
        RectTransform selectedPanel_rt = panels[selectedPanelIndex].GetComponent<RectTransform> ();
        RectTransform activePanel_rt = panels[activePanelIndex].GetComponent<RectTransform> ();

        isPanelmoving = true;
        panels[selectedPanelIndex].SetActive (true);
        buttons[activePanelIndex].GetComponent<Animator> ().SetBool ("IsSelected", false);
        buttons[activePanelIndex].GetComponent<Animator> ().SetTrigger ("Normal");

        float percent = 0;        

        // sicne selectedPanel is moving from hide position to show position from right to left, use minus
        float hidePos = selectedPanel_rt.anchoredPosition.x;
        float showPos = hidePos - selectedPanel_rt.sizeDelta.x;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / time;

            // animate the path
            float x1 = Mathf.Lerp (hidePos, showPos, percent);
            float x2 = Mathf.Lerp (showPos, hidePos, percent);

            selectedPanel_rt.anchoredPosition = new Vector2 (x1, selectedPanel_rt.anchoredPosition.y);
            activePanel_rt.anchoredPosition = new Vector2 (x2, selectedPanel_rt.anchoredPosition.y);

            // fade in and out
            selectedPanel_cg.alpha = Mathf.Lerp (0, 1f, percent);
            activePanel_cg.alpha = Mathf.Lerp (1f, 0, percent);
            
            // when finished moving, set the previous panel to inactive and update index
            if (percent >= 1)
            {
                isPanelmoving = false;
                panels[activePanelIndex].SetActive (false);
                buttons[selectedPanelIndex].GetComponent<Animator> ().SetBool ("IsSelected", true);
                buttons[selectedPanelIndex].GetComponent<Animator> ().SetTrigger ("Highlighted");
                activePanelIndex = selectedPanelIndex;                
            }

            yield return null;
        }
    }





    //void activateSelectedPanel(int panelIndex)
    //{
    //    for (int i = 0; i < panels.Length; i++)
    //    {
    //        if (i == panelIndex)
    //            panels[i].SetActive (true);
    //        else
    //            panels[i].SetActive (false);
    //    }
    //}







}
