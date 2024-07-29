using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    //"Global" variables
    public bool AnyPopUpsOpen;


    //Other Variables
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlots;
    public Transform itemSlotsParentWhenInDrugCabinet;
    public Transform itemSlotsParentWhenNotInDrugCabinet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        menuActivated = false;
        //Debug.Log("MenuActivated = "+ menuActivated);    
    }

    // Update is called once per frame
    void Update()
    {

        AnyPopUpsOpen = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().AnyPopUpsOpen;

        if (AnyPopUpsOpen)
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        if ( Input.GetButtonDown("Inventory") && menuActivated && !AnyPopUpsOpen) 
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated && !AnyPopUpsOpen) 
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
