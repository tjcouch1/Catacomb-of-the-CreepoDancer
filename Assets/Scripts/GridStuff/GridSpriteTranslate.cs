﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpriteTranslate : MonoBehaviour
{
	[Range(0f,1f)]
	public float translateTime = .1f;
	float translateCurrent = 0f;
	Vector3 transformPrev;

	// Use this for initialization
	void Start()
	{
		transformPrev = transform.parent.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (translateCurrent < translateTime)
		{
			SetPosition();

			translateCurrent += Time.deltaTime;
		}
		//else
		//	transform.position = transform.parent.transform.position;
	}

	public void StartTranslation()
	{
		translateCurrent = 0f;
		transformPrev = transform.parent.transform.position;
		SetPosition();
	}

	void SetPosition()
	{
		transform.position = transformPrev + (transform.parent.transform.position - transformPrev) * Mathf.Min(translateCurrent / translateTime, 1);
	}
}
