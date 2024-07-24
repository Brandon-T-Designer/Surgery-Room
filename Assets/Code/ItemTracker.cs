using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    //"Global" Variables
    public bool AnyPopUPsOpen = false;
    public ItemSlot[] itemSlots;

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
        AnyPopUPsOpen = false;
    }

    //Yeah we know it works already
    /* // Update is called once per frame
    void Update()
    {       
        //Debug.Log(Have_Red);
        //Debug.Log(Have_Blue);
        if ( (Have_Red && Have_Blue) == true)
        {
            //Debug.Log("Success!");
        }     
    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks if Player has necessary items for Procedure A
        if (collision.gameObject.tag == "Player")
        {
            Have_Red = CheckForItems("Red Pills");
            Have_Blue = CheckForItems("Blue Pills"); 
        }
        bool Procedure_A_Materials = Have_Red && Have_Blue;

        //Opens Popup if Player has necessary items for Procedure A
        if ( collision.gameObject.tag == "Player" && ( (Procedure_A_Materials == true)||(SurgeryTableWasOpened) ) )
        {
            popupWindow.SetActive(true);
            AnyPopUPsOpen = true;
            SurgeryTableWasOpened = true;

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            popupWindow.SetActive(false);
            AnyPopUPsOpen = false;
        }
    }
    private bool CheckForItems(string TargetItem)
    {
        itemSlots = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().itemSlots;
        for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].itemName == TargetItem)
                {                   
                    return true;
                }
            }  
            return false;
    }
}
