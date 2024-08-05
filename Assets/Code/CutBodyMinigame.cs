using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutBodyMinigame : MonoBehaviour
{
	public float duration;
	public GameObject gameOverScreen;
	public GameObject mustCauterizePanel;
	public float circleCollidersSeparation;
	public float circleColliderRadius;
	Move_Body body;
	float timer;

	void OnEnable ()
	{
		Time.timeScale = 0;
	}

	void Update ()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			if (!mustCauterizePanel.activeSelf)
			{
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				if (!body.cutStartZoneBoxCollider.bounds.Contains(mousePosition))
				{
					mustCauterizePanel.SetActive(true);
					
				}
			}
			else
			{

			}
		}
		else if (Mouse.current.rightButton.wasPressedThisFrame)
		{
			if (!mustCauterizePanel.activeSelf)
			{
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				if (body.cutEndZoneBoxCollider.bounds.Contains(mousePosition))
				{
					Time.timeScale = 1;
					Destroy(body.gameObject);
				}
				else
				{
					mustCauterizePanel.SetActive(true);
				}
			}
			else
			{
				
			}
		}
		timer += Time.deltaTime;
		if (timer >= duration)
		{
			gameOverScreen.SetActive(true);
			mustCauterizePanel.SetActive(false);
			enabled = false;
		}
	}

	public void ResetLevel ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
