using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodVial : MonoBehaviour
{
	public void OnFilled ()
	{
        BloodDrawMinigame.instance.OnBloodVialsFilled ();
	}
}
