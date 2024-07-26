using UnityEngine;

public class WhenStartButtonPressed : MonoBehaviour
{
    //Other Variables
    public bool HasGameStarted;
    public bool IsThisPopUpOpen = true;
    public GameObject GameStartBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasGameStarted == true)
        {
            GameStartBackground.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void StartGame() 
    {
        HasGameStarted = true;
        IsThisPopUpOpen = false;
    }
}
    