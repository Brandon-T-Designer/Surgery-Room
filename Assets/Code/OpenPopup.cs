using UnityEngine;

public class OpenPopup : MonoBehaviour
{
    //"Global" Variables
    public bool IsThisPopUpOpen = false;

    //Game Objects
    public GameObject popupWindow;

    void Start()
    {
        popupWindow.SetActive(false);
        IsThisPopUpOpen = false;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            popupWindow.SetActive(true);
            IsThisPopUpOpen = true;
        }
    }

    /*
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            popupWindow.SetActive(false);
            IsThisPopUpOpen = false;
        }
    }
    */

    public virtual void IfClicked() 
    {
        Debug.Log("Activated!!!");

            popupWindow.SetActive(false);
            IsThisPopUpOpen = false;
            Time.timeScale = 1;
    }
}
