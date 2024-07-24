using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemtoInventory : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager2;
    public GameObject InventoryIsFull2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager2 = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //Debug.Log("Collided");
            //Debug.Log("Inventory Checker 1000" + inventoryManager.AddItemToInventory(itemName, quantity, sprite, itemDescription));

            if (inventoryManager2.AddItemToInventory(itemName, quantity, sprite, itemDescription))
            {
                //Debug.Log("Destoryed");
                Destroy(gameObject);
            }
        }
    }
    private void ItemClicked2()
    {
        if (!inventoryManager2.AddItemToInventory(itemName, quantity, sprite, itemDescription))
        {
            InventoryIsFull2.SetActive(true);
        }
    }
}

