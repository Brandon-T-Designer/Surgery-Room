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
	List<Vector3> cutPointsLeftToCauterize = new List<Vector3>();
	bool releasedLeftMouseButtonSinceStartCauterize;
	Vector2 previousMousePosition;
	float distanceToNextCutPoint;
	Grabbable previousGrabbed;
	float cauterizeTimer;

	void OnEnable ()
	{
		Time.timeScale = 0;
		lineRenderer.startWidth = cutPointRadius;
		lineRenderer.endWidth = cutPointRadius;
		procedureCanvasGos[GlobalVariableCommandCenter.instance.ProcedureNumber].SetActive(true);
	}

	void Update ()
	{
		Physics2D.SyncTransforms();
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		if (mustCauterizePanel.activeSelf)
		{
			cauterizeTimer -= Time.unscaledDeltaTime;
			cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
			if (cauterizeTimer <= 0)
				GameOver ();
			else if (Mouse.current.leftButton.wasReleasedThisFrame && !releasedLeftMouseButtonSinceStartCauterize)
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
				bool hittingCollider = false;
				for (int i = 0; i < colliders.Count; i ++)
				{
					Collider2D collider = colliders[i];
					if (hitColliders.Contains(collider))
					{
						hittingCollider = true;
						break;
					}
				}
				if (!hittingCollider)
					ResetCauterize ();
				else
				{
					for (int i = 0; i < hitColliders.Count; i ++)
					{
						Collider2D hitCollider = hitColliders[i];
						if (colliders.Contains(hitCollider) && hitCollider is CircleCollider2D)
						{
							int indexOfHitCollider = collidersLeftToCauterize.IndexOf(hitCollider);
							if (indexOfHitCollider != -1)
							{
								collidersLeftToCauterize.RemoveAt(indexOfHitCollider);
								cutPointsLeftToCauterize.RemoveAt(indexOfHitCollider);
								lineRenderer.positionCount = cutPointsLeftToCauterize.Count;
								lineRenderer.SetPositions(cutPointsLeftToCauterize.ToArray());
								if (cutPointsLeftToCauterize.Count == 0)
									StopCauterize ();
							}
						}
					}
				}
			}
			else
				ResetCauterize ();
		}
		else if (Grabbable.currentGrabbed != null && Grabbable.currentGrabbed == previousGrabbed && Grabbable.currentGrabbed.id == "Scalpel")
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				if (!cutStartZoneBoxCollider.OverlapPoint(mousePosition))
					StartCauterize ();
			}
			if (Mouse.current.leftButton.isPressed)
			{
				if (mousePosition.y < cutStartZoneBoxCollider.bounds.min.y || mousePosition.y > cutStartZoneBoxCollider.bounds.max.y)
					StartCauterize ();
				else
				{
					distanceToNextCutPoint -= (mousePosition - previousMousePosition).magnitude;
					if (distanceToNextCutPoint <= 0)
					{
						while (distanceToNextCutPoint <= 0)
						{
							Vector2 previousCutPoint = previousMousePosition;
							if (cutPoints.Count > 0)
								previousCutPoint = cutPoints[cutPoints.Count - 1];
							float distanceToPreviousCutPoint = (mousePosition - previousCutPoint).magnitude;
							Vector2 cutPoint = previousCutPoint + Vector2.ClampMagnitude((mousePosition - previousMousePosition).normalized * cutPointSeparation, distanceToPreviousCutPoint);
							if (cutPoints.Count > 0)
							{
								BoxCollider2D boxCollider = new GameObject().AddComponent<BoxCollider2D>();
								boxCollider.transform.position = (cutPoint + previousCutPoint) / 2;
								boxCollider.transform.rotation = Quaternion.LookRotation(Vector3.forward, cutPoint - previousCutPoint);
								boxCollider.transform.localScale = new Vector3(cutPointRadius, Mathf.Min(cutPointSeparation, (cutPoint - previousCutPoint).magnitude));
								colliders.Add(boxCollider);
								cutPoints.Add(cutPoint);
							}
							CircleCollider2D circleCollider = new GameObject().AddComponent<CircleCollider2D>();
							circleCollider.transform.position = cutPoint;
							circleCollider.transform.localScale = Vector3.one * cutPointRadius;
							colliders.Add(circleCollider);
							cutPoints.Add(cutPoint);
							distanceToNextCutPoint += cutPointSeparation;
						}
						ResetCauterize ();
					}
				}
			}
			else if (Mouse.current.leftButton.wasReleasedThisFrame)
			{
				if (cutEndZoneBoxCollider.OverlapPoint(mousePosition))
				{
					Time.timeScale = 1;
					Destroy(body.gameObject);
					gameObject.SetActive(false);
					procedureCanvasGos[GlobalVariableCommandCenter.instance.ProcedureNumber].SetActive(false);
					for (int i = 0; i < colliders.Count; i ++)
					{
						Collider2D collider = colliders[i];
						Destroy(collider.gameObject);
					}
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
		cauterizeTimerText.gameObject.SetActive(true);
		releasedLeftMouseButtonSinceStartCauterize = false;
		ResetCauterize ();
	}

	void StopCauterize ()
	{
		mustCauterizePanel.SetActive(false);
		cauterizeTimerText.gameObject.SetActive(false);
		for (int i = 0; i < colliders.Count; i ++)
		{
			Collider2D collider = colliders[i];
			Destroy(collider.gameObject);
		}
		colliders.Clear();
		cutPoints.Clear();
	}

	void ResetCauterize ()
	{
		collidersLeftToCauterize = new List<Collider2D>(colliders);
		cutPointsLeftToCauterize = new List<Vector3>(cutPoints);
		for (int i = 0; i < collidersLeftToCauterize.Count; i ++)
		{
			Collider2D collider = collidersLeftToCauterize[i];
			if (collider is BoxCollider2D)
			{
				collidersLeftToCauterize.RemoveAt(i);
				cutPointsLeftToCauterize.RemoveAt(i);
				i --;
			}
		}
		if (cutPoints.Count == 1)
		{
			lineRenderer.positionCount = 2;
			lineRenderer.SetPositions(new Vector3[] { cutPoints[0], cutPoints[0] + Vector3.right * cutPointSeparation });
		}
		else
		{
			lineRenderer.positionCount = cutPointsLeftToCauterize.Count;
			lineRenderer.SetPositions(cutPointsLeftToCauterize.ToArray());
		}
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
