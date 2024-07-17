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
        bool AnyPopUPsOpen_SurgeryTable = GameObject.Find("SurgeryTable").GetComponent<ItemTracker>().AnyPopUPsOpen;
        bool AnyPopUPsOpen_DrugCabinet = GameObject.Find("DrugCabinet").GetComponent<Medication_Bench>().AnyPopUPsOpen;

        AnyPopUPsOpen = (AnyPopUPsOpen_SurgeryTable || AnyPopUPsOpen_DrugCabinet);

        //bool AnyPopUPsOpen = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().AnyPopUPsOpen;
    }
}
