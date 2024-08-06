using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour
{
	public Transform trs;
	public string id;
	public static Grabbable currentGrabbed;
	Vector2 initPosition;

	void Awake ()
	{
		initPosition = trs.position;
	}

	public void Grab ()
	{
		if (currentGrabbed != null)
			currentGrabbed.Drop ();
		enabled = true;
		currentGrabbed = this;
	}

	void Drop ()
	{
		trs.position = initPosition;
		enabled = false;
	}

	void Update ()
	{
		trs.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		if (Mouse.current.rightButton.wasPressedThisFrame)
			Drop ();
	}
}
