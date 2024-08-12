using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class WhenStartButtonPressed : MonoBehaviour
{
    public GameObject GameStartCanvas;
    public void StartGame()
    {
        GameStartCanvas.GetComponent<GameStartupScript>().StartGame();
    }
}
    