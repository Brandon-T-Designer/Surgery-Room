using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    //"Global" variables
    public bool AnyPopUPsOpen;


    //Other Variables
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlots;

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

    public bool AddItemToInventory(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {

        //Debug.Log("Activated");
        //Debug.Log(itemSlots.Length);
        //Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);


        for (int i = 0; i < itemSlots.Length; i++)
        {
            //Debug.Log("Start Logic");
            if (itemSlots[i].isFull == false)
            {
                itemSlots[i].AddItemToSlot(itemName, quantity, itemSprite, itemDescription);

                //Debug.Log("This Slot is Empty");
                //Debug.Log("THIS IS itemSlot "+ itemSlot);
                return true;
            }
            
        
        }
        return false;
    }

    public void DeselectAllSlots() 
    {
        //Debug.Log("Deslecting");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false); 
            itemSlots[i].thisItemSelected = false;
        }
    }
}
