using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour
{
	public Transform trs;
	public string id;
	public static Grabbable currentGrabbed;
	public Image image;
	public Canvas canvas;
	[HideInInspector]
	public Vector2 defaultPosition;

	void Awake ()
	{
		defaultPosition = trs.position;
	}

	public void Grab ()
	{
		if (currentGrabbed != null)
			currentGrabbed.Drop ();
		currentGrabbed = this;
		enabled = true;
		image.raycastTarget = false;
	}

	public void Drop ()
	{
		image.raycastTarget = true;
		trs.position = defaultPosition;
		enabled = false;
		StartCoroutine(DropRoutine ());
	}

	IEnumerator DropRoutine ()
	{
		yield return new WaitForFixedUpdate();
		currentGrabbed = null;
	}

	void Update ()
	{
        Physics2D.SyncTransforms();
		Vector2 position = Mouse.current.position.ReadValue();
		if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
			position = Camera.main.ScreenToWorldPoint(position);
		trs.position = new Vector3(position.x, position.y, trs.position.z);
		if (Mouse.current.leftButton.wasReleasedThisFrame)
			Drop ();
	}
}
