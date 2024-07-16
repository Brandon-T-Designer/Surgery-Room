using UnityEngine;

public class Medication_Bench : MonoBehaviour
{
    //"Global" Variables
    public bool AnyPopUPsOpen = false;

    //Game Objects
    public GameObject popupWindow;

    void Start()
    {
        popupWindow.SetActive(false);
        AnyPopUPsOpen = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Doctor")
        {
            popupWindow.SetActive(true);
            AnyPopUPsOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Doctor")
        {
            popupWindow.SetActive(false);
            AnyPopUPsOpen = false;

        }
    }
}