using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string ItemName = "Red Pills";
        bool Have_Blue = false;

        ItemName = "Red Pills";
        bool Have_Red = false;

        //Make sure to add an OnCollisionTrigger conditional to the surgery table
        //Also find out how to access the InventoryManager array in this script 
        /*
    bool Activate_Find_Item = GameObject.Find("Doctor").GetComponent<ItemTracker>().Activate_Find_Item; 
        string TargetItem = GameObject.Find("ItemTracker").GetComponent<TargetItem>();
        public bool Find_Item()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].itemName == TargetItem)
                {
                    return true;
                }
            }
            return false;
        }
    */




    }
}
