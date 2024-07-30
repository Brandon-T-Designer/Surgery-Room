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
            Debug.Log("1");
            bool IsThisPopUpOpen_StartButton = GameObject.Find("GameStartCanvas").GetComponent<GameStartupScript>().IsThisPopUpOpen;
            
            Debug.Log("2");
            bool IsThisPopUpOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().IsThisPopUpOpen;

            Debug.Log("3");
            bool IsThisPopUpOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().IsThisPopUpOpen;

            Debug.Log("4");
            bool IsThisPopUpOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopup>().IsThisPopUpOpen;
            
            Debug.Log("5");
            bool IsThisPopUpOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<OpenPopupSurgeryTable>().IsThisPopUpOpen;

            Debug.Log("6");
            //Logicz
            AnyPopUpsOpen = ( IsThisPopUpOpen_StartButton || IsThisPopUpOpen_OrganFridge || IsThisPopUpOpen_DrugCabinet || IsThisPopUpOpen_BloodStation || IsThisPopUpOpen_SurgeryTable);

            Debug.Log("7");
            //ItemSlot
            itemSlots = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().itemSlots;

            Debug.Log("8");
        //HasGameStarted 
        //bool HasGameStarted_StartAndEndScripts = GameObject.Find("StartAndEndScripts").GetComponent<StartGame>().HasGameStarted;
        //bool HasGameStarted_StartButton = GameObject.Find("StartButton").GetComponent<StartGame>().HasGameStarted;

        //Logicz
        //HasGameStarted = HasGameStarted_StartButton;
        //GameOver


    }
}
