using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutBodyMinigame : MonoBehaviour
{
	public float cauterizeDuration;
	public GameObject gameOverScreen;
	public GameObject mustCauterizePanel;
	public LineRenderer lineRenderer;
	public float cutPointSeparation;
	public float cutPointRadius;
	public TMP_Text cauterizeTimerText;
	public GameObject[] procedureCanvasGos;
    public BoxCollider2D cutStartZoneBoxCollider;
    public BoxCollider2D cutEndZoneBoxCollider;
	public static Move_Body body;
	List<Collider2D> colliders = new List<Collider2D>();
	List<Collider2D> collidersLeftToCauterize = new List<Collider2D>();
	List<Vector3> cutPoints = new List<Vector3>();
	bool releasedLeftMouseButtonSinceStartCauterize;
	Vector2 previousMousePosition;
	float distanceToNextCutPoint;
	Grabbable previousGrabbed;
	float cauterizeTimer;

	void OnEnable ()
	{
		Time.timeScale = 0;
		cutPoints.Clear();
		lineRenderer.startWidth = cutPointRadius;
		lineRenderer.endWidth = cutPointRadius;
		procedureCanvasGos[GlobalVariableCommandCenter.instance.ProcedureNumber].SetActive(true);
	}

	void Update ()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		if (mustCauterizePanel.activeSelf)
		{
			cauterizeTimer += Time.deltaTime;
			cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
			if (cauterizeTimer <= 0)
				GameOver ();
			else if (Mouse.current.leftButton.wasReleasedThisFrame)
				releasedLeftMouseButtonSinceStartCauterize = true;
			else if (releasedLeftMouseButtonSinceStartCauterize && Mouse.current.leftButton.isPressed && Grabbable.currentGrabbed != null && Grabbable.currentGrabbed.id == "Electrocauterizer")
			{
				RaycastHit2D[] hits = Physics2D.LinecastAll(previousMousePosition, mousePosition);
				List<Collider2D> hitColliders = new List<Collider2D>();
				for (int i = 0; i < hits.Length; i ++)
				{
					RaycastHit2D hit = hits[i];
					hitColliders.Add(hit.collider);
				}
				if (!hitColliders.Contains(collidersLeftToCauterize[0]) && !hitColliders.Contains(collidersLeftToCauterize[collidersLeftToCauterize.Count - 1]))
				{
					bool isMouseOverSecondCutPoint = collidersLeftToCauterize[1].OverlapPoint(mousePosition);
					if (!isMouseOverSecondCutPoint && !hitColliders.Contains(collidersLeftToCauterize[collidersLeftToCauterize.Count - 2]))
						collidersLeftToCauterize = colliders;
					else
					{
						if (isMouseOverSecondCutPoint)
							collidersLeftToCauterize.RemoveAt(0);
						else
							collidersLeftToCauterize.RemoveAt(collidersLeftToCauterize.Count - 1);
						if (collidersLeftToCauterize.Count == 1)
						{
							collidersLeftToCauterize.Clear();
							mustCauterizePanel.SetActive(false);
							cauterizeTimerText.gameObject.SetActive(false);
						}
					}
				}
			}
		}
		else if (Grabbable.currentGrabbed != null && Grabbable.currentGrabbed == previousGrabbed && Grabbable.currentGrabbed.id == "Scalpel")
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				if (!cutStartZoneBoxCollider.bounds.Contains(mousePosition))
					StartCauterize ();
				else
				{
					cutPoints.Clear();
					cutPoints.Add(mousePosition);
					CircleCollider2D circleCollider = new GameObject().AddComponent<CircleCollider2D>();
					circleCollider.transform.position = mousePosition;
					circleCollider.transform.localScale = Vector3.one * cutPointRadius;
					colliders.Add(circleCollider);
				}
			}
			else if (Mouse.current.leftButton.isPressed)
			{
				distanceToNextCutPoint -= (mousePosition - previousMousePosition).magnitude;
				if (distanceToNextCutPoint <= 0)
				{
					while (distanceToNextCutPoint <= 0)
					{
						Vector2 cutPoint = previousMousePosition + (mousePosition - previousMousePosition).normalized * cutPointSeparation;
						if (cutPoints.Count > 1)
						{
							BoxCollider2D boxCollider = new GameObject().AddComponent<BoxCollider2D>();
							Vector2 previousCutPoint = cutPoints[cutPoints.Count - 1];
							boxCollider.transform.position = (cutPoint + previousCutPoint) / 2;
							boxCollider.transform.rotation = Quaternion.LookRotation(Vector3.forward, cutPoint - previousCutPoint);
							boxCollider.transform.localScale = new Vector3(cutPointRadius, cutPointSeparation);
							colliders.Add(boxCollider);
						}
						CircleCollider2D circleCollider = new GameObject().AddComponent<CircleCollider2D>();
						circleCollider.transform.position = cutPoint;
						circleCollider.transform.localScale = Vector3.one * cutPointRadius;
						colliders.Add(circleCollider);
						cutPoints.Add(cutPoint);
						distanceToNextCutPoint += cutPointSeparation;
					}
					lineRenderer.SetPositions(cutPoints.ToArray());
				}
			}
			else if (Mouse.current.rightButton.wasPressedThisFrame)
			{
				if (cutEndZoneBoxCollider.bounds.Contains(mousePosition))
				{
					Time.timeScale = 1;
					Destroy(body.gameObject);
					enabled = false;
					procedureCanvasGos[GlobalVariableCommandCenter.instance.ProcedureNumber].SetActive(false);
				}
				else
					StartCauterize ();
			}
		}
		previousMousePosition = mousePosition;
		previousGrabbed = Grabbable.currentGrabbed;
	}

	void StartCauterize ()
	{
		mustCauterizePanel.SetActive(true);
		cauterizeTimer = cauterizeDuration;
		cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
		cauterizeTimerText.gameObject.SetActive(false);
		releasedLeftMouseButtonSinceStartCauterize = false;
		collidersLeftToCauterize = colliders;
	}

	void GameOver ()
	{
		gameOverScreen.SetActive(true);
		mustCauterizePanel.SetActive(false);
		cauterizeTimerText.gameObject.SetActive(false);
		enabled = false;
	}

	public void ResetLevel ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
