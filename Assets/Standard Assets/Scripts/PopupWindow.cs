using UnityEngine;

public class PopupWindow : MonoBehaviour
{
    public GameObject popupWindow;

    void Start ()
    {
        popupWindow.gameObject.SetActive(true);
    }
}
