//using JetBrains.Annotations;
using UnityEngine;

public class GlobalVariableCommandCenter : MonoBehaviour
{
    //"Global" Variables 
    public bool AnyPopUpsOpen;
    public ItemSlot[] itemSlots;   
    public int ProcedureNumber = 0;

    //To be developed further later
    public bool StationIsOccupied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        AnyPopUpsOpen = false;
        StationIsOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {

        //AnyPopUPsOpen 
            bool IsThisPopUpOpen_StartButton = GameObject.Find("GameStartCanvas").GetComponent<GameStartupScript>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().IsThisPopUpOpen;
            bool IsThisPopUpOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopupBloodStation>().IsThisPopUpOpen;     
            bool IsThisPopUpOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<OpenPopupSurgeryTable>().IsThisPopUpOpen;

            //Logicz
            AnyPopUpsOpen = (IsThisPopUpOpen_StartButton || IsThisPopUpOpen_OrganFridge || IsThisPopUpOpen_DrugCabinet || IsThisPopUpOpen_BloodStation || IsThisPopUpOpen_SurgeryTable);

        //ItemSlot
            itemSlots = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().itemSlots;

        //Procedure Number

            //See Function SetProcedureNumber

        //StationIsOccupied

            //See Function SetStationIsOccupied
    }

    //Procedure Number
    public void SetProcedureNumber(int SetProcedureNumber)
    {
        ProcedureNumber = SetProcedureNumber;
        Debug.Log("ProcedureNumber is " + ProcedureNumber);
    }

    public void SetStationIsOccupied(bool SetStationIsOccupied)
    {
        StationIsOccupied = SetStationIsOccupied;
        Debug.Log("StationIsOccupied is " + StationIsOccupied);
    }

}
