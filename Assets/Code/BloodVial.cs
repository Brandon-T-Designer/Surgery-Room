using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodVial : MonoBehaviour
{
	public void OnFilled ()
	{
		if (enabled)
        	BloodDrawMinigame.instance.OnBloodVialsFilled ();
	}
}
