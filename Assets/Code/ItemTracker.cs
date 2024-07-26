using UnityEngine;

public class ItemTracker : MonoBehaviour
{   
    //"Global" Variables
    public bool IsThisPopUpOpen = false;
    public ItemSlot[] itemSlots;
    //public InventoryManager inventoryManager;

    public bool SurgeryTableWasOpened = false;
    public GameObject popupWindow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Inventory Item Status    
    //"Red Pills"
    bool Have_Red = false;

    //"Blue Pills"
    bool Have_Blue = false;

    //Start Function Not Necessary 
    void Start()
    {
        popupWindow.SetActive(false);
        IsThisPopUpOpen = false;
    }

    //Yeah we know it works already
     // Update is called once per frame
    void Update()
    {
        //Debug.Log(Have_Red);
        //Debug.Log(Have_Blue);
        /*
        if ( (Have_Red && Have_Blue) == true)
        {
            //Debug.Log("Success!");
        } 
        */

        if (IsThisPopUpOpen == true) 
        {
            IsThisPopUpOpen =  GameObject.Find("SurgeryTable").GetComponent<OpenPopup>().IsThisPopUpOpen;
            //Debug.Log("Popup State" + IsThisPopUpOpen);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Procedure A

            //Checks if Player has necessary items for Procedure A
            if (collision.gameObject.tag == "Player")
            {
                //Debug.Log("Collided!");
                Have_Red = CheckForItems("Red Pills");
                Have_Blue = CheckForItems("Blue Pills"); 
            }
            bool Procedure_A_Materials = Have_Red && Have_Blue;

            //Opens Popup if Player has necessary items for Procedure A
            if ( collision.gameObject.tag == "Player" && ( (Procedure_A_Materials == true)||(SurgeryTableWasOpened == true) ) )
            {
                //Debug.Log("Opened!");
                popupWindow.SetActive(true);
                IsThisPopUpOpen = true;
                SurgeryTableWasOpened = true;

                Time.timeScale = 0;
            }

        //Procedure B

    }

    /*
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            popupWindow.SetActive(false);
            IsThisPopUpOpen = false;
        }
    }
    */


    private bool CheckForItems(string TargetItem)
    {
        itemSlots = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().itemSlots;
        for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].itemName == TargetItem)
                {
                    //Removes Necessary Items from Inventory ONLY if the Surgery Table has never been opened before.
                    if (SurgeryTableWasOpened == false) 
                    {
                        //Remove the necessary items and set SurgeryTableWasOpened to true.
                        RemoveItem(TargetItem);
                    }
                                      
                    
                    return true;
                }
            }  
            return false;
    }

    private void RemoveItem(string TargetItem)
    { 
        bool StopLoop = false;
        itemSlots = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().itemSlots;
        for (int i = 0; ( (i < itemSlots.Length) && (StopLoop == false) ) ; i++)
        {
            if (itemSlots[i].itemName == TargetItem)
            {
                itemSlots[i].EmptySlot();
                itemSlots[i].RemoveItemFromSlot(itemSlots[i].itemName, itemSlots[i].quantity, itemSlots[i].itemSprite, itemSlots[i].itemDescription);
                
                StopLoop = true;
            }
        }
        
    }
    
}
