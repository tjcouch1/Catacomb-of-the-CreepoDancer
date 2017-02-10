using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicImageScalar : MonoBehaviour {

	float start_x;
	RectTransform rt;

	// Use this for initialization
	void Awake() {
		rt = GetComponent<RectTransform>();
		start_x = rt.sizeDelta.x;
	}
	
	// Should be from 0-1
	public void SetXScale(float scl) {
		rt.sizeDelta = new Vector2((scl * start_x), rt.sizeDelta.y);
	}
}
