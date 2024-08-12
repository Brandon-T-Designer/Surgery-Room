using UnityEngine;

public class WhenRestartButtonPressed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject GameLoseCanvas;
    public void RestartGame()
    {
        GameLoseCanvas.GetComponent<GameLoseScript>().RestartGame();
    }
}
