using JetBrains.Annotations;
using UnityEngine;

public class GlobalVariableCommandCenter : MonoBehaviour
{
    public static GlobalVariableCommandCenter instance;
    //"Global" Variables 
    public bool AnyPopUpsOpen;
    public ItemSlot[] itemSlots;   
    public int ProcedureNumber = 0;
    public bool StationIsOccupied;
    public int WhichBodyInTreatment;
    public Transform[] tables;
    public bool[] tablesOccupied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        instance = this;
        AnyPopUpsOpen = false;
        StationIsOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {

        //AnyPopUPsOpen 
            bool IsThisPopUpOpen_GameStartCanvas = GameObject.Find("GameStartCanvas").GetComponent<GameStartupScript>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_GameLoseCanvas = GameObject.Find("GameLoseCanvas").GetComponent<GameLoseScript>().IsThisPopUpOpen;

            bool IsThisPopUpOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopupBloodStation>().IsThisPopUpOpen;     
            bool IsThisPopUpOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<OpenPopupSurgeryTable>().IsThisPopUpOpen;

            //Logicz
            AnyPopUpsOpen = (IsThisPopUpOpen_GameStartCanvas || IsThisPopUpOpen_GameLoseCanvas || IsThisPopUpOpen_OrganFridge || IsThisPopUpOpen_DrugCabinet || IsThisPopUpOpen_BloodStation || IsThisPopUpOpen_SurgeryTable);

        //ItemSlot
            // itemSlots = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().itemSlots;
            itemSlots = InventoryManager.instance.itemSlots;

        //Procedure Number

            //See Function SetProcedureNumber

        //StationIsOccupied

            //See Function SetStationIsOccupied

        //Game Ended
    }

    //Procedure Number
    public void SetProcedureNumber(int SetProcedureNumber)
    {
        ProcedureNumber = SetProcedureNumber;
        Debug.Log("ProcedureNumber is " + ProcedureNumber);
    }

    //StationIsOccupied
    public void SetStationIsOccupied(bool SetStationIsOccupied)
    {
        StationIsOccupied = SetStationIsOccupied;
        Debug.Log("StationIsOccupied is " + StationIsOccupied);
    }

    public void SetWhichBodyInTreatment(int SetWhichBodyInTreatment)
    {
        WhichBodyInTreatment = SetWhichBodyInTreatment;
        Debug.Log("WhichBodyInTreatment is " + WhichBodyInTreatment);
    }


}
