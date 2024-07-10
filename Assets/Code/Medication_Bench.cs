using UnityEngine;

public class Medication_Bench : MonoBehaviour
{
    public GameObject popupWindow;

    void Start()
    {
        popupWindow.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Doctor")
        {
            popupWindow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Doctor")
        {
            popupWindow.SetActive(false);
        }
    }
}