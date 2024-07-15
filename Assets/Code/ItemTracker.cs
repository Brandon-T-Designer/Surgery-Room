using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Inventory Item Status    
    //"Red Pills"
    bool Have_Red = false;

    //"Blue Pills"
    bool Have_Blue = false;

    //Start Function Not Necessary 
    //void Start(){ }

    // Update is called once per frame

    public ItemSlot itemSlotScript;
    public int itemSlotLength;
    public InventoryManager InventoryManagerScript;
    void Update()
    {

        Debug.Log(Have_Red);
        Debug.Log(Have_Blue);
        if ( (Have_Red && Have_Blue) == true)
        {
            Debug.Log("Success!");
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Have_Red = CheckForItems("Red Pills");
            Have_Blue = CheckForItems("Blue Pills");
        }
    }
    private bool CheckForItems(string TargetItem)
    {
     ItemSlot[] itemSlots = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().itemSlot;
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
