using UnityEngine;

public class WhenInstructionButtonPressed : MonoBehaviour
{
public GameObject GameStartMenu;  
public GameObject Tutorial;
   
   public void ActivateTutorial()
   {
		GameStartMenu.SetActive(false);		
		Tutorial.SetActive(true);	
   }
}
