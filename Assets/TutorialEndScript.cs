using UnityEngine;

public class TutorialEndScript : MonoBehaviour
{
    public GameObject CurrentPanel;
    public GameObject Panel1;
    public GameObject TutorialCanvas;
    public GameObject NextPanel;

    public void TransitionToNextPanel()
    {
        CurrentPanel.SetActive(false);
        Panel1.SetActive(true);
        TutorialCanvas.SetActive(false);
        NextPanel.SetActive(true);
    }
}
