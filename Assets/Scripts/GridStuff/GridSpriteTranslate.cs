using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpriteTranslate : MonoBehaviour
{
	[Range(0f,1f)]
	public float translateTime = .1f;
	float translateCurrent = 0f;
	Vector3 transformPrev;

	public enum MoveType {NULL, WALK, JUMP};
	float jumpHeight = .5f;

	MoveType currentMoveType = MoveType.JUMP;

	// Use this for initialization
	void Start()
	{
		transformPrev = transform.parent.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		translateCurrent += Time.deltaTime;
		SetPosition(currentMoveType);
	}

	public void StartTranslation()
	{
		translateCurrent = 0f;
		transformPrev = transform.parent.transform.position;
	}

	public void SetPosition()
	{
		SetPosition(MoveType.JUMP);
	}

	public void SetPosition(MoveType m)
	{
		currentMoveType = m;
		transform.position = transformPrev + (transform.parent.transform.position - transformPrev) * Mathf.Min(translateCurrent / translateTime, 1);
		transform.position = new Vector3(transform.position.x, transform.position.y + PathY(m), 0);
	}

	float PathY(MoveType m)
	{
		switch (m)
		{
		case MoveType.JUMP:
			if (translateCurrent > 0 && translateCurrent < translateTime)
				return jumpHeight - Mathf.Pow((translateCurrent - translateTime / 2), 2) * 4 * jumpHeight / Mathf.Pow(translateTime, 2);
			else
				return 0;
		default:
			return 0;
		}
	}
}
