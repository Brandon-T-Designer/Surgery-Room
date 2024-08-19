using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutBodyMinigame : MonoBehaviour
{
	public float cauterizeDuration;
	public GameObject mustCauterizePanel;
	public GameObject mustCutPanel;
	public GameObject liverGo;
	public LineRenderer correctCutLineRenderer;
	public LineRenderer incorrectCutLineRenderer;
	public float cutPointSeparation;
	public float cutPointRadius;
	public TMP_Text cauterizeTimerText;
	public GameObject[] procedureCanvasGos;
    public BoxCollider2D cutStartZoneBoxCollider;
    public BoxCollider2D cutEndZoneBoxCollider;
	public AudioSource ouchAudioSource;
	public GameObject liverTransplantPatientCutOpenWithBadLiverGo;
	public GameObject liverTransplantPatientCutOpenWithoutLiverGo;
	public GameObject liverTransplantPatientCutOpenWithNewLiverGo;
	public GameObject liverTransplantPatientSewnUpGo;
	public GameObject appendicitisPatientCutOpenWithBadAppendixGo;
	public GameObject appendicitisPatientCutOpenWithoutAppendixGo;
	public GameObject appendicitisPatientSewnUpGo;
	public Collider2D organDisposaleCollider;
	public Collider2D liverPlacementCollider;
	public Collider2D openStomachCollider;
	public static Move_Body body;
	List<LineRenderer> correctCutLineRenderers = new List<LineRenderer>();
	List<LineRenderer> incorrectCutLineRenderers = new List<LineRenderer>();
	List<Collider2D> colliders = new List<Collider2D>();
	List<Collider2D> collidersLeftToCauterize = new List<Collider2D>();
	List<Vector3> correctCutPoints = new List<Vector3>();
	List<Vector3> incorrectCutPoints = new List<Vector3>();
	List<Vector3> cutPointsLeftToCauterize = new List<Vector3>();
	Vector2 previousMousePosition;
	float distanceToNextCutPoint;
	bool startedCutFromEndZone;
	Grabbable previousGrabbed;
	bool isDoneWithCutting;
	bool disposedBadOrgan;
	Vector2 mousePosition;
	float cauterizeTimer;
	bool placedNewOrgan;

	void OnEnable ()
	{
		Time.timeScale = 0;
		placedNewOrgan = false;
		disposedBadOrgan = false;
		isDoneWithCutting = false;
		correctCutLineRenderer.startWidth = cutPointRadius;
		correctCutLineRenderer.endWidth = cutPointRadius;
		incorrectCutLineRenderer.startWidth = cutPointRadius;
		incorrectCutLineRenderer.endWidth = cutPointRadius;
		procedureCanvasGos[GlobalVariableCommandCenter.instance.ProcedureNumber].SetActive(true);
		if (GlobalVariableCommandCenter.instance.ProcedureNumber == 1)
			liverGo.SetActive(true);
	}

	void Update ()
	{
		Physics2D.SyncTransforms();
		mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		HandleSurgery ();
		if (Grabbable.currentGrabbed != null && Grabbable.currentGrabbed == previousGrabbed && Grabbable.currentGrabbed.id == "Scalpel")
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				startedCutFromEndZone = cutEndZoneBoxCollider.OverlapPoint(mousePosition);
				if (!startedCutFromEndZone && !cutStartZoneBoxCollider.OverlapPoint(mousePosition))
				{
					SetCutLineRendererPoints (false);
					StartCauterize ();
				}
			}
			if (Mouse.current.leftButton.isPressed)
			{
				if (mustCauterizePanel.activeSelf || mousePosition.x < cutStartZoneBoxCollider.bounds.min.x || mousePosition.x > cutEndZoneBoxCollider.bounds.max.x || mousePosition.y < cutStartZoneBoxCollider.bounds.min.y || mousePosition.y > cutStartZoneBoxCollider.bounds.max.y)
				{
					Cut (false);
					if (!mustCauterizePanel.activeSelf)
						StartCauterize ();
				}
				else
					Cut (true);
			}
			else if (!mustCauterizePanel.activeSelf && Mouse.current.leftButton.wasReleasedThisFrame)
			{
				if ((!startedCutFromEndZone && cutEndZoneBoxCollider.OverlapPoint(mousePosition)) || (startedCutFromEndZone && cutStartZoneBoxCollider.OverlapPoint(mousePosition)))
				{
					OnDoneWithCutting ();
					if (GlobalVariableCommandCenter.instance.ProcedureNumber == 1)
						liverTransplantPatientCutOpenWithBadLiverGo.SetActive(true);
					else if (GlobalVariableCommandCenter.instance.ProcedureNumber == 2)
						appendicitisPatientCutOpenWithBadAppendixGo.SetActive(true);
				}
				else
					StartCauterize ();
			}
		}
		if (mustCauterizePanel.activeSelf)
		{
			cauterizeTimer -= Time.unscaledDeltaTime;
			cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
			if (cauterizeTimer <= 0)
				GameOver ();
			else if (Mouse.current.leftButton.isPressed && Grabbable.currentGrabbed != null && Grabbable.currentGrabbed == previousGrabbed && Grabbable.currentGrabbed.id == "Electrocauterizer")
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
					SetCutLineRendererPoints (false);
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
								incorrectCutLineRenderer.positionCount = cutPointsLeftToCauterize.Count;
								incorrectCutLineRenderer.SetPositions(cutPointsLeftToCauterize.ToArray());
								if (cutPointsLeftToCauterize.Count == 0)
									StopCauterize ();
							}
						}
					}
				}
			}
			else
				SetCutLineRendererPoints (false);
		}
		previousMousePosition = mousePosition;
		previousGrabbed = Grabbable.currentGrabbed;
	}

	void HandleSurgery ()
	{
		if (isDoneWithCutting && Grabbable.currentGrabbed != null && Mouse.current.leftButton.wasReleasedThisFrame)
		{
			if (!disposedBadOrgan && organDisposaleCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
			{
				Destroy(Grabbable.currentGrabbed.gameObject);
				disposedBadOrgan = true;
				if (GlobalVariableCommandCenter.instance.ProcedureNumber == 1 && Grabbable.currentGrabbed.id == "Bad Liver")
				{
					liverTransplantPatientCutOpenWithoutLiverGo.SetActive(true);
					Destroy(Grabbable.currentGrabbed.gameObject);
				}
				else if (GlobalVariableCommandCenter.instance.ProcedureNumber == 2 && Grabbable.currentGrabbed.id == "Bad Appendix")
				{
					appendicitisPatientCutOpenWithoutAppendixGo.SetActive(true);
					Destroy(Grabbable.currentGrabbed.gameObject);
				}
			}
			else if (!placedNewOrgan)
			{
				if (GlobalVariableCommandCenter.instance.ProcedureNumber == 1 && Grabbable.currentGrabbed.id == "Liver" && liverPlacementCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
				{
					liverTransplantPatientCutOpenWithoutLiverGo.SetActive(false);
					liverTransplantPatientCutOpenWithNewLiverGo.SetActive(true);
					placedNewOrgan = true;
					Destroy(Grabbable.currentGrabbed.gameObject);
				}
				else if (GlobalVariableCommandCenter.instance.ProcedureNumber == 2 && Grabbable.currentGrabbed.id == "Sutures" && openStomachCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
				{
					appendicitisPatientCutOpenWithoutAppendixGo.SetActive(false);
					appendicitisPatientSewnUpGo.SetActive(true);
					Grabbable.currentGrabbed.Drop ();
				}
			}
			else if (GlobalVariableCommandCenter.instance.ProcedureNumber == 1 && Grabbable.currentGrabbed.id == "Sutures" && openStomachCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
			{
				liverTransplantPatientCutOpenWithNewLiverGo.SetActive(true);
				liverTransplantPatientSewnUpGo.SetActive(true);
			}
		}
	}

	void OnDoneWithCutting ()
	{
		for (int i = 0; i < colliders.Count; i ++)
		{
			Collider2D collider = colliders[i];
			Destroy(collider.gameObject);
		}
		Grabbable.currentGrabbed.Drop ();
		mustCutPanel.SetActive(false);
		isDoneWithCutting = true;
	}

	public void OnSurgeryTableLeft ()
	{
		OnDoneWithCutting ();
		liverTransplantPatientCutOpenWithBadLiverGo.SetActive(false);
		liverTransplantPatientCutOpenWithoutLiverGo.SetActive(false);
		liverTransplantPatientCutOpenWithNewLiverGo.SetActive(false);
		liverTransplantPatientSewnUpGo.SetActive(false);
		appendicitisPatientCutOpenWithBadAppendixGo.SetActive(false);
		appendicitisPatientCutOpenWithoutAppendixGo.SetActive(false);
		appendicitisPatientSewnUpGo.SetActive(false);
	}

	void Cut (bool isCorrect)
	{
		LineRenderer lineRenderer = incorrectCutLineRenderer;
		List<Vector3> cutPoints = incorrectCutPoints;
		if (isCorrect)
		{
			lineRenderer = correctCutLineRenderer;
			cutPoints = correctCutPoints;
		}
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
				if (!isCorrect)
				{
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
				}
				cutPoints.Add(cutPoint);
				distanceToNextCutPoint += cutPointSeparation;
			}
		}
		if (!isCorrect)
		{
			collidersLeftToCauterize = new List<Collider2D>(colliders);
			cutPointsLeftToCauterize = new List<Vector3>(incorrectCutPoints);
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
		}
		SetCutLineRendererPoints (isCorrect);
	}

	void StartCauterize ()
	{
		ouchAudioSource.Play();
		mustCauterizePanel.SetActive(true);
		mustCutPanel.SetActive(false);
		cauterizeTimer = cauterizeDuration;
		cauterizeTimerText.text = "Time left to cauterize: " + cauterizeTimer.ToString("F1");
		cauterizeTimerText.gameObject.SetActive(true);
	}

	void StopCauterize ()
	{
		mustCauterizePanel.SetActive(false);
		mustCutPanel.SetActive(true);
		cauterizeTimerText.gameObject.SetActive(false);
		for (int i = 0; i < colliders.Count; i ++)
		{
			Collider2D collider = colliders[i];
			Destroy(collider.gameObject);
		}
		colliders.Clear();
		correctCutPoints.Clear();
		correctCutLineRenderer.positionCount = 0;
		incorrectCutPoints.Clear();
	}

	void SetCutLineRendererPoints (bool isCorrect)
	{
		LineRenderer lineRenderer = incorrectCutLineRenderer;
		List<Vector3> cutPoints = cutPointsLeftToCauterize;
		if (isCorrect)
		{
			lineRenderer = correctCutLineRenderer;
			cutPoints = correctCutPoints;
		}
		if (cutPoints.Count == 1)
		{
			lineRenderer.positionCount = 2;
			lineRenderer.SetPositions(new Vector3[] { cutPoints[0], cutPoints[0] + Vector3.right * cutPointSeparation });
		}
		else
		{
			lineRenderer.positionCount = cutPoints.Count;
			lineRenderer.SetPositions(cutPoints.ToArray());
		}
	}

	void GameOver ()
	{
		GameLoseScript.instance.LoseTheGame ();
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
