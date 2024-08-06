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
	public static Move_Body body;
	List<Collider2D> colliders = new List<Collider2D>();
	List<Vector3> cutPoints = new List<Vector3>();
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
		body.cutPathIndicatorsParentGo.SetActive(true);
	}

	void Update ()
	{
		if (Grabbable.currentGrabbed != previousGrabbed)
			return;
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			if (Grabbable.currentGrabbed.id == "Scalpel")
			{
				if (!mustCauterizePanel.activeSelf)
				{
					if (!body.cutStartZoneBoxCollider.bounds.Contains(mousePosition))
						StartCauterize ();
				}
				else
				{
					RaycastHit2D[] hits = Physics2D.LinecastAll(previousMousePosition, mousePosition);
					List<Collider2D> hitColliders = new List<Collider2D>();
					for (int i = 0; i < hits.Length; i ++)
					{
						RaycastHit2D hit = hits[i];
						hitColliders.Add(hit.collider);
					}
					if (!hitColliders.Contains(colliders[0]) && !hitColliders.Contains(colliders[colliders.Count - 1]))
					{
						bool isMouseOverSecondCutPoint = colliders[1].OverlapPoint(mousePosition);
						if (!isMouseOverSecondCutPoint && !hitColliders.Contains(colliders[colliders.Count - 2]))
							GameOver ();
						else
						{
							if (isMouseOverSecondCutPoint)
								colliders.RemoveAt(0);
							else
								colliders.RemoveAt(colliders.Count - 1);
							if (colliders.Count == 1)
							{
								colliders.Clear();
								mustCauterizePanel.SetActive(false);
								cauterizeTimerText.gameObject.SetActive(false);
							}
						}
					}
				}
			}
		}
		else if (Mouse.current.rightButton.wasPressedThisFrame)
		{
			if (Grabbable.currentGrabbed.id == "Scalpel")
			{
				if (!mustCauterizePanel.activeSelf)
				{
					if (body.cutEndZoneBoxCollider.bounds.Contains(mousePosition))
					{
						Time.timeScale = 1;
						Destroy(body.gameObject);
						enabled = false;
					}
					else
						StartCauterize ();
				}
				else
				{
					
				}
			}
		}
		if (mustCauterizePanel.activeSelf)
		{
			cauterizeTimer += Time.deltaTime;
			cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
			if (cauterizeTimer <= 0)
				GameOver ();
		}
		else if (Mouse.current.leftButton.isPressed && Grabbable.currentGrabbed.id == "Electrocauterizer")
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
		previousMousePosition = mousePosition;
		previousGrabbed = Grabbable.currentGrabbed;
	}

	void StartCauterize ()
	{
		mustCauterizePanel.SetActive(true);
		cauterizeTimer = cauterizeDuration;
		cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
		cauterizeTimerText.gameObject.SetActive(false);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		cutPoints.Add(mousePosition);
		CircleCollider2D circleCollider = new GameObject().AddComponent<CircleCollider2D>();
		circleCollider.transform.position = mousePosition;
		circleCollider.transform.localScale = Vector3.one * cutPointRadius;
		colliders.Add(circleCollider);
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
