using UnityEngine;

public class GlobalVariableCommandCenter : MonoBehaviour
{
    //"Global" Variables 
    public bool AnyPopUPsOpen;
    public ItemSlot[] itemSlots;
    //public bool HasGameStarted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnyPopUPsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        //AnyPopUPsOpen 
            bool AnyPopUPsOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().AnyPopUPsOpen;
            bool AnyPopUPsOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().AnyPopUPsOpen;
            bool AnyPopUPsOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopup>().AnyPopUPsOpen;
            bool AnyPopUPsOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<ItemTracker>().AnyPopUPsOpen;

            //Logicz
            AnyPopUPsOpen = (AnyPopUPsOpen_OrganFridge || AnyPopUPsOpen_DrugCabinet || AnyPopUPsOpen_BloodStation || AnyPopUPsOpen_SurgeryTable);

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
