using UnityEngine;

public class GameStartupScript : MonoBehaviour
{
    //"Global" variables
    public bool IsThisPopUpOpen = true;

    //Other Variables
    public GameObject GameStartBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (IsThisPopUpOpen == false)
        {
            GameStartBackground.SetActive(false);
            Time.timeScale = 1;
        }
        */
    }

    public void StartGame()
    {
        Debug.Log("StartGame was Activated!");
        GameStartBackground.SetActive(false);
        IsThisPopUpOpen = false;
        Time.timeScale = 1;
    }
}
