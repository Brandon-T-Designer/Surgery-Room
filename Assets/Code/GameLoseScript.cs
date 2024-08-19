using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoseScript : MonoBehaviour
{
    public static GameLoseScript instance;
    //"Global" variables
    public bool IsThisPopUpOpen = false;

    //Other Variables
    public GameObject GameLoseBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
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

    public void LoseTheGame()
    {
        Time.timeScale = 0;
        GameLoseBackground.SetActive(true);
        IsThisPopUpOpen = true;
        Debug.Log("HAHAHA LOSER!");
    }

    public void RestartGame()
    {
        Debug.Log("Let's ReZero this shit");
        //GameLoseBackground.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

