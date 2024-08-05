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
	public float circleColliderRadius;
	List<Vector2> cutPoints = new List<Vector2>();
	float distanceToNextCutPoint;
	float previousMousePosition;
	float cauterizeTimer;
	Move_Body body;

	void OnEnable ()
	{
		Time.timeScale = 0;
		cutPoints.Clear();
		lineRenderer.startWidth = circleColliderRadius;
		lineRenderer.endWidth = circleColliderRadius;
	}

	void Update ()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			if (!mustCauterizePanel.activeSelf)
			{
				if (!body.cutStartZoneBoxCollider.bounds.Contains(mousePosition))
					StartCauterize ();
			}
			else
			{
				
			}
		}
		else if (Mouse.current.rightButton.wasPressedThisFrame)
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
		if (mustCauterizePanel.activeSelf)
		{
			cauterizeTimer += Time.deltaTime;
			if (cauterizeTimer >= cauterizeDuration)
			{
				gameOverScreen.SetActive(true);
				mustCauterizePanel.SetActive(false);
				enabled = false;
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
					cutPoints.Add(cutPoint);
					distanceToNextCutPoint += cutPointSeparation;
				}
				lineRenderer.SetPositions(cutPoints.ToArray());
			}
		}
		previousMousePosition = mousePosition;
	}

	void StartCauterize ()
	{
		mustCauterizePanel.SetActive(true);
		cauterizeTimer = 0;
	}

	public void ResetLevel ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
