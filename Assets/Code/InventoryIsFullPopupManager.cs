using UnityEngine;
using System.Collections;

public class InventoryIsFullPopupManager : MonoBehaviour
{
    public GameObject InventoryIsFullPopup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Routine Started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown()
    {
        StartCoroutine(CloseInventoryIsFullPopup());
    }

    public IEnumerator CloseInventoryIsFullPopup()
    {
        yield return new WaitForSecondsRealtime(2);
        InventoryIsFullPopup.SetActive(false);
        Debug.Log("Routine Ended");
    }
}
