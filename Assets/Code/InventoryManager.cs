using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    //"Global" variables
    public bool AnyPopUPsOpen;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuActivated = false;
        //Debug.Log("MenuActivated = "+ menuActivated);    
    }

    // Update is called once per frame
    void Update()
    {

        AnyPopUPsOpen = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().AnyPopUPsOpen;

        if (AnyPopUPsOpen)
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        if ( Input.GetButtonDown("Inventory") && menuActivated && !AnyPopUPsOpen) 
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        
        else if (Input.GetButtonDown("Inventory") && !menuActivated && !AnyPopUPsOpen) 
        {
            //Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        
        //Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                //Debug.Log("THIS IS itemSlot "+ itemSlot);
                return true;
            }
        
        }
        return false;
    }

    public void DeselectAllSlots() 
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false); 
            itemSlot[i].thisItemSelected = false;
        }
    }
}
