using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinOscelator : MonoBehaviour {

	public float x_float;
	public float y_float;
	public float frequency;

	float accumulatedTime;
	float start_x;
	float start_y;

	// Use this for initialization
	void Awake () {
		start_x = transform.position.x;
		start_y = transform.position.y;
		accumulatedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		accumulatedTime += Time.deltaTime;
		if(accumulatedTime > frequency) {
			accumulatedTime = 0;
		}
		float tstep = (accumulatedTime / frequency) * Mathf.PI * 2;
		float x = 0, y = 0;
		x = (Mathf.Sin(tstep) * x_float) + start_x;
		y = (Mathf.Sin(tstep) * y_float) + start_y;
		transform.position = new Vector3(x, y, 0);
	}
}
