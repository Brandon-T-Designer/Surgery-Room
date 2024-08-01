using JetBrains.Annotations;
using UnityEngine;

public class OpenPopupSurgeryTable : MonoBehaviour
{
    //"Global" Variables
    public bool IsThisPopUpOpen = false;
    public ItemSlot[] itemSlots;
    public int ProcedureNumber;
    //public InventoryManager inventoryManager;

    public bool SurgeryTableWasOpened = false;
    public GameObject popupWindow;

    void Start()
    {
        //popupWindow.SetActive(false);
        //IsThisPopUpOpen = false;
    }

    //Yeah we know it works already
    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Get Procedure Number
        

        //Checks if Player has necessary items for Procedure A
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided!");
            ProcedureNumber = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().ProcedureNumber;
            Debug.Log("Procedure Number Needed " + ProcedureNumber);

            if (ProcedureNumber == 1)
            {
                //Liver Transplant
                ProcedureCheck("Red Pills", "Cyclosporine", "Liver");
            }
            else if (ProcedureNumber == 2)
            {
                //Appendicitis
                ProcedureCheck("Red Pills", "null", "null");
            }
            else if (ProcedureNumber == 3)
            {
                //Gallstone Disease
                ProcedureCheck("Metrondiazole", "null", "null");
            }
            else
            {
                //ProcedureCheck("Red Pills", "Blue Pills", "null");
            }
            
        }
    }

    public void ProcedureCheck(string item_1, string item_2, string item_3)
    {
        //Debug.Log("Collided!");
        bool Have_item_1 = CheckForItem(item_1);
        bool Have_item_2 = CheckForItem(item_2);
        bool Have_item_3 = CheckForItem(item_3);

        Have_item_1 = NullChecker(item_1, Have_item_1);
        Have_item_2 = NullChecker(item_2, Have_item_2);
        Have_item_3 = NullChecker(item_3, Have_item_3);

        bool Procedure_Materials = Have_item_1 && Have_item_2 && Have_item_3;

        //Removes Necessary Items from Inventory ONLY if the Surgery Table has never been opened before.
        if ((Procedure_Materials == true) && (SurgeryTableWasOpened == false))
        {
            RemoveItem(item_1);
            RemoveItem(item_2);
            RemoveItem(item_3);
        }

        //Opens Popup if Player has necessary items for Procedure A
        if (((Procedure_Materials == true) || (SurgeryTableWasOpened == true)))
        {
            popupWindow.SetActive(true);
            IsThisPopUpOpen = true;
            SurgeryTableWasOpened = true;
            Time.timeScale = 0;
        }
    }

    private bool CheckForItem(string TargetItem)
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

    public bool NullChecker(string TestItem, bool TestBool)
    {
        if (TestItem == "null")
        {
            return true;
        }
        return TestBool;
    }

    private void RemoveItem(string TargetItem)
    {
        bool StopLoop = false;
        itemSlots = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().itemSlots;
        for (int i = 0; ((i < itemSlots.Length) && (StopLoop == false)); i++)
        {
            if (itemSlots[i].itemName == TargetItem)
            {
                itemSlots[i].EmptySlot();
                itemSlots[i].RemoveItemFromSlot(itemSlots[i].itemName, itemSlots[i].quantity, itemSlots[i].itemSprite, itemSlots[i].itemDescription);

                StopLoop = true;
            }
        }

    }

    public virtual void IfClicked()
    {
        //Debug.Log("Activated!!!");

        popupWindow.SetActive(false);
        IsThisPopUpOpen = false;
        Time.timeScale = 1;

        //Gilead Code
        /*
        InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.gameObject.SetActive(false);
        for (int i = 0; i < InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.childCount; i++)
        {
            Transform itemSlotTrs = InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.GetChild(i);
            itemSlotTrs.SetParent(InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet);
            itemSlotTrs.localScale = Vector3.one;
            i--;
        }
        */
    }
}
