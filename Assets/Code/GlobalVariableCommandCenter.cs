using UnityEngine;

public class GlobalVariableCommandCenter : MonoBehaviour
{
    //"Global" Variables 
    public bool AnyPopUpsOpen;
    public ItemSlot[] itemSlots;
    //public bool HasGameStarted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnyPopUpsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        //AnyPopUPsOpen 
            bool IsThisPopUpOpen_StartButton = GameObject.Find("GameStartCanvas").GetComponent<GameStartupScript>().IsThisPopUpOpen;

            bool IsThisPopUpOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopup>().IsThisPopUpOpen;
            
            bool IsThisPopUpOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<OpenPopupSurgeryTable>().IsThisPopUpOpen;

            //Logicz
            AnyPopUpsOpen = (IsThisPopUpOpen_StartButton || IsThisPopUpOpen_OrganFridge || IsThisPopUpOpen_DrugCabinet || IsThisPopUpOpen_BloodStation || IsThisPopUpOpen_SurgeryTable);

            //ItemSlot
            itemSlots = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().itemSlots;

        //HasGameStarted 
        //bool HasGameStarted_StartAndEndScripts = GameObject.Find("StartAndEndScripts").GetComponent<StartGame>().HasGameStarted;
        //bool HasGameStarted_StartButton = GameObject.Find("StartButton").GetComponent<StartGame>().HasGameStarted;

        //Logicz
        //HasGameStarted = HasGameStarted_StartButton;
        //GameOver


    }
}
