using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //======ITEM DATA======//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    //======ITEM SLOT======//
    [SerializeField]
    private Image itemImage;

    //======ITEM DESCRIPTION SLOT======//
    public Image ItemDescriptionImage;
    //public Image Background;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected = false; 

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItemToSlot(string itemName, int quantity, Sprite itemSprite, string itemDescription) 
    {
        //Debug.Log("Item Added");
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;     
        
        itemImage.sprite = itemSprite;

        if (thisItemSelected == true)
        {
            FillSlot();
        }
    }

    //Added Stuff
    public void RemoveItemFromSlot(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = "";
        this.quantity = 0;
        this.itemSprite = emptySprite;
        this.itemDescription = "";
        isFull = false;

        itemImage.sprite = emptySprite;
    }
    //Added Stuff End

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();    
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {

        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;

        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = itemSprite;

        if (ItemDescriptionImage.sprite == null)
        {
            ItemDescriptionImage.sprite = emptySprite;
            //ItemDescriptionImage.sprite = Background.sprite;
        }

    }

   
    public void OnRightClick()
    {
        if (thisItemSelected) 
        {
            this.quantity -= 1;
            if (this.quantity <= 0) 
            {
                EmptySlot();
                RemoveItemFromSlot(itemName, quantity, itemSprite, itemDescription);
            }
        }     
    }

   public void EmptySlot()
   {
       itemImage.sprite = emptySprite;
       ItemDescriptionNameText.text = "";
       ItemDescriptionText.text = "";
       ItemDescriptionImage.sprite = emptySprite;
       //ItemDescriptionImage.sprite = Background.sprite;
    }

    private void FillSlot()
    {
        itemImage.sprite = itemSprite;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = itemSprite;

    }



}
