using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public GameObject inventoryUI;
    public Transform itemsParent;    

    Inventory inventory;
    InventorySlot[] slots;

    void Awake ()
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning ("Please assign inventoryUI");
        }

        if (itemsParent == null)
        {
            Debug.LogWarning ("Please assign itemsParent");
        }

        // Disable inventory UI on default
        inventoryUI.SetActive (false);
    }


	void Start ()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot> ();
	}
		
	void Update () {
		if (Input.GetButtonDown ("Inventory"))
        {
            inventoryUI.SetActive (!inventoryUI.activeSelf);
        }
	}

    void UpdateUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i<inventory.items.Count)
            {
                slots[i].AddItem (inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot ();
            }
        }
    }
}
