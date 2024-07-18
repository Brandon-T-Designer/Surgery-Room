using UnityEngine;

public class OpenPopup : MonoBehaviour
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            popupWindow.SetActive(true);
            AnyPopUPsOpen = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            popupWindow.SetActive(false);
            AnyPopUPsOpen = false;
        }
    }
}
