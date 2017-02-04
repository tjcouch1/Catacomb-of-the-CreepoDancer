using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			transform.position += new Vector3(1f, 0f, 0f);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			transform.position += new Vector3(-1f, 0f, 0f);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			transform.position += new Vector3(0f, 1f, 0f);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			transform.position += new Vector3(0f, -1f, 0f);
		}
	}
}
