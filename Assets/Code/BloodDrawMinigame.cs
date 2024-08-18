using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BloodDrawMinigame : MonoBehaviour
{
	public Transform needleTrs;
	public Transform targetTrs;
	public Transform needleMinYPositionTrs;
	public Transform needleMaxYPositionTrs;
	public BoxCollider2D targetBoxCollider;
	public GameObject bloodVialsParentGo;
	public GameObject[] bloodTypesGos;
	public GameObject bloodBagsParentGo;
	public float needleMoveSpeed;
	public float needleMoveToTargetDuration;
	public float needleMoveAwayFromTargetDuration;
	[HideInInspector]
	public int currentBloodTypeIndex;
	[HideInInspector]
	public List<string> bloodBagNames = new List<string>();
	public static BloodDrawMinigame instance;
	float needleXDistanceToTarget;
	Vector2Int needleMoveDirection;
	float initNeedleXLocalPosition;
	int previousNeedleMoveYDirection;

	void OnEnable ()
	{
		instance = this;
		Time.timeScale = 0;
		if (initNeedleXLocalPosition == 0)
			initNeedleXLocalPosition = needleTrs.localPosition.x;
		needleTrs.localPosition = new Vector2(initNeedleXLocalPosition, Random.Range(needleMinYPositionTrs.localPosition.y, needleMaxYPositionTrs.localPosition.y));
		needleXDistanceToTarget = Mathf.Abs(needleTrs.localPosition.x - targetTrs.localPosition.x);
		needleMoveDirection = Vector2Int.up;
		if (Random.value < .5f)
			needleMoveDirection.y *= -1;
		bloodVialsParentGo.SetActive(false);
		bloodBagsParentGo.SetActive(false);
		AddItemtoInventory[] addItemToInventorys = bloodBagsParentGo.GetComponentsInChildren<AddItemtoInventory>();
		for (int i = 0; i < addItemToInventorys.Length; i ++)
		{
			AddItemtoInventory addItemToInventory = addItemToInventorys[i];
			bloodBagNames.Add(addItemToInventory.itemName);
		}
		for (int i = 0; i < bloodTypesGos.Length; i ++)
		{
			GameObject bloodTypeGo = bloodTypesGos[i];
			bloodTypeGo.SetActive(false);
		}
	}

	void Update ()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame && needleMoveDirection.x == 0)
		{
			previousNeedleMoveYDirection = needleMoveDirection.y;
			needleMoveDirection.x = -1;
			needleMoveDirection.y = 0;
		}
		if (needleMoveDirection.x < 0)
		{
			needleTrs.localPosition = new Vector2(Mathf.Lerp(needleTrs.localPosition.x, targetTrs.localPosition.x, needleXDistanceToTarget / needleMoveToTargetDuration * Time.unscaledDeltaTime * (1f / Mathf.Abs(needleTrs.localPosition.x - targetTrs.localPosition.x))), needleTrs.localPosition.y);
			if (needleTrs.localPosition.x == targetTrs.localPosition.x)
			{
				if (needleTrs.position.y > targetBoxCollider.bounds.min.y && needleTrs.position.y < targetBoxCollider.bounds.max.y)
				{
					gameObject.SetActive(false);
					bloodVialsParentGo.SetActive(true);
				}
				needleMoveDirection.x *= -1;
			}
		}
		else if (needleMoveDirection.x > 0)
		{
			needleTrs.localPosition = new Vector2(Mathf.Lerp(needleTrs.localPosition.x, initNeedleXLocalPosition, needleXDistanceToTarget / needleMoveAwayFromTargetDuration * Time.unscaledDeltaTime * (1f / Mathf.Abs(needleTrs.localPosition.x - initNeedleXLocalPosition))), needleTrs.localPosition.y);
			if (needleTrs.localPosition.x == initNeedleXLocalPosition)
			{
				needleMoveDirection.x = 0;
				needleMoveDirection.y = previousNeedleMoveYDirection;
			}
		}
		if (needleTrs.localPosition.y > needleMaxYPositionTrs.localPosition.y)
			needleMoveDirection.y = -1;
		else if (needleTrs.localPosition.y < needleMinYPositionTrs.localPosition.y)
			needleMoveDirection.y = 1;
		needleTrs.localPosition = new Vector2(needleTrs.localPosition.x, needleTrs.localPosition.y + needleMoveSpeed * needleMoveDirection.y * Time.unscaledDeltaTime);
	}

	public void OnBloodVialsFilled ()
	{
		currentBloodTypeIndex = Random.Range(0,bloodTypesGos.Length);
		bloodTypesGos[currentBloodTypeIndex].SetActive(true);
		bloodBagsParentGo.SetActive(true);
	}
}
