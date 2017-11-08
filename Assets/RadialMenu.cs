using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RadialMenu : MonoBehaviour {

    List<Button> buttons = new List<Button> ();
    bool isOpen = false;
    int buttonDistance = 150;

    void Awake ()
    {
        buttons = GetComponentsInChildren<Button> ().Where(x => x.gameObject.transform.parent!=transform.parent).ToList();
        GetComponent<Button> ().onClick.AddListener (() => { MenuButtonOnClick (); });

        foreach (Button b in buttons)
        {
            b.gameObject.SetActive (false);
        }
    }

    void Start () {
		
	}
	
	public void MenuButtonOnClick ()
    {
        isOpen = !isOpen;
        float angle = 360 / (buttons.Count) * Mathf.Rad2Deg;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (isOpen)
            {
                float xPos = Mathf.Cos (angle * i) * buttonDistance;
                float yPos = Mathf.Sin (angle * i) * buttonDistance;

                buttons[i].transform.position = new Vector2 (transform.position.x + xPos, transform.position.y + yPos);
                buttons[i].gameObject.SetActive (true);
            }
            else
            {
                buttons[i].transform.position = transform.position;
                buttons[i].gameObject.SetActive (false);
            }
        }
    }
}
