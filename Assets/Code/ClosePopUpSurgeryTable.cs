using UnityEngine;

public class ClosePopUpSurgeryTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //"Global Variables"
    public bool AnyPopUPsOpen = false;
    public GameObject Station;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IfClicked()
    {
        //Debug.Log("IfClicked was Activated");
        Station.GetComponent<OpenPopupSurgeryTable>().IfClicked();
    }
}
