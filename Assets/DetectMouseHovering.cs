using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DetectMouseHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string itemName; 
    public TMP_Text itemNameText; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemNameText.text = itemName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemNameText.text = "";
    }

    /*
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
    */
    
}
