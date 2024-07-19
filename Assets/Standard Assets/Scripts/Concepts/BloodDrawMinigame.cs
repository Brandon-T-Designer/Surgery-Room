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
	public float needleMoveSpeed;
	public float needleMoveToTargetDuration;
	public float needleMoveAwayFromTargetDuration;
	float needleXDistanceToTarget;
	Vector2Int needleMoveDirection;
	float initNeedleXLocalPosition;
	int previousNeedleMoveYDirection;

	void OnEnable ()
	{
		Time.timeScale = 0;
		needleXDistanceToTarget = Mathf.Abs(needleTrs.localPosition.x - targetTrs.localPosition.x);
		initNeedleXLocalPosition = needleTrs.localPosition.x;
		needleTrs.localPosition = new Vector2(initNeedleXLocalPosition, Random.Range(needleMinYPositionTrs.localPosition.y, needleMaxYPositionTrs.localPosition.y));
		needleMoveDirection = Vector2Int.up;
		if (Random.value < .5f)
			needleMoveDirection.y *= -1;
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
					Time.timeScale = 1;
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
}
