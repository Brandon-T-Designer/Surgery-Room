using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
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
        Station.GetComponent<OpenPopup>().IfClicked();
    }
}