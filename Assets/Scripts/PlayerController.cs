using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class PlayerController : MonoBehaviour {

	GridMovement gm;

	// Use this for initialization
	void Awake () {
		gm = GetComponent<GridMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Rightwards Movement
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if(gm.CanMove(GridMovement.Directions.RIGHT)){
				transform.position += new Vector3(1f, 0f, 0f);
			}
		}

		// Leftwards Movement
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if(gm.CanMove(GridMovement.Directions.LEFT)){
				transform.position += new Vector3(-1f, 0f, 0f);
			}
		}

		// Upwards Movement
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(gm.CanMove(GridMovement.Directions.UP)){
				transform.position += new Vector3(0f, 1f, 0f);
			}
		}

		// Downwards Movement
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(gm.CanMove(GridMovement.Directions.DOWN)){
				transform.position += new Vector3(0f, -1f, 0f);
			}
		}
	}
}
