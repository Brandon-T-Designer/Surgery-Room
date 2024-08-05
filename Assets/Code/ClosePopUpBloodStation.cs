using UnityEngine;

public class ClosePopUpBloodStation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //"Global Variables"
    public GameObject Station;

    public void IfClicked()
    {
        //Debug.Log("IfClicked was Activated");
        Station.GetComponent<OpenPopupBloodStation>().IfClicked();
    }
}
