using UnityEngine;

public class OpenPopup : MonoBehaviour
{
    //"Global" Variables
    public bool IsThisPopUpOpen = false;

    //Game Objects
    public GameObject popupWindow;

    void Start()
    {
        popupWindow.SetActive(false);
        IsThisPopUpOpen = false;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            popupWindow.SetActive(true);
            IsThisPopUpOpen = true;

            //Gilead Code
            InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.gameObject.SetActive(true);
            for (int i = 0; i < InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet.childCount; i ++)
            {
                Transform itemSlotTrs = InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet.GetChild(i);
                itemSlotTrs.SetParent(InventoryManager.instance.itemSlotsParentWhenInDrugCabinet);
                itemSlotTrs.localScale = Vector3.one;
                i --;
            }
        }
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

    public virtual void IfClicked() 
    {
        //Debug.Log("Activated!!!");

            popupWindow.SetActive(false);
            IsThisPopUpOpen = false;
            Time.timeScale = 1;

        //Gilead Code
        InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.gameObject.SetActive(false);
        for (int i = 0; i < InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.childCount; i ++)
        {
            Transform itemSlotTrs = InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.GetChild(i);
            itemSlotTrs.SetParent(InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet);
            itemSlotTrs.localScale = Vector3.one;
            i --;
        }
    }
}
