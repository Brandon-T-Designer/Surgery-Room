using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject CurrentPanel;
    public GameObject NextPanel;

    public void TransitionToNextPanel()
    {
        CurrentPanel.SetActive(false);
        NextPanel.SetActive(true);
    }
}
