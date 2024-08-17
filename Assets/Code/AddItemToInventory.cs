using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddItemtoInventory : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;
    public GameObject InventoryIsFullPopup;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.Find("InventoryCanvas") != null)
            inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        else
            inventoryManager = InventoryManager.instance;
    }

    public void ItemClicked()
    {
        if (!inventoryManager.AddItemToInventory(itemName, quantity, sprite, itemDescription))
        {
            InventoryIsFullPopup.SetActive(true);
            InventoryIsFullPopup.GetComponent<InventoryIsFullPopupManager>().StartCountdown();
        }
    }
}

