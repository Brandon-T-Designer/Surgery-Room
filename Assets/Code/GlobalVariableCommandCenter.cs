using UnityEngine;

public class GlobalVariableCommandCenter : MonoBehaviour
{
    //"Global" Variables 
    public bool AnyPopUPsOpen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnyPopUPsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        bool AnyPopUPsOpen_OrganFridge = GameObject.Find("OrganFridge").GetComponent<OpenPopup>().AnyPopUPsOpen;
        bool AnyPopUPsOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<OpenPopup>().AnyPopUPsOpen;
        bool AnyPopUPsOpen_BloodStation = GameObject.Find("BloodStation").GetComponent<OpenPopup>().AnyPopUPsOpen;

        bool AnyPopUPsOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<ItemTracker>().AnyPopUPsOpen;

        //Logicz
        AnyPopUPsOpen = (AnyPopUPsOpen_OrganFridge || AnyPopUPsOpen_DrugCabinet || AnyPopUPsOpen_BloodStation || AnyPopUPsOpen_SurgeryTable);

        //bool AnyPopUPsOpen = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().AnyPopUPsOpen;
    }
}
