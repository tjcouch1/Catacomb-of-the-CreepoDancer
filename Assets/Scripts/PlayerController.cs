using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class PlayerController : MonoBehaviour {

	public GameObject updater;
	GridMovement gm;
	SimultaneousUpdater smu;

	// Use this for initialization
	void Awake () {
		gm = GetComponent<GridMovement>();
		smu = updater.GetComponent<SimultaneousUpdater>();
	}

	void Start() 
	{
		if(smu == null) {
			Debug.Log("Updater Isn't configured correctly.");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		GridMovement.Directions dir = GridMovement.Directions.NULL;

		// Rightwards Movement
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			dir = GridMovement.Directions.RIGHT;
		}

		// Leftwards Movement
		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			dir = GridMovement.Directions.LEFT;
		}

		// Upwards Movement
		else if(Input.GetKeyDown(KeyCode.UpArrow)){
			dir = GridMovement.Directions.UP;
		}

		// Downwards Movement
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			dir = GridMovement.Directions.DOWN;
		}

		if(dir != GridMovement.Directions.NULL) {

			// Fire off a world update
			smu.UpdateWorld();

			// Check for movement
			if(gm.CanMove(dir)) {
				transform.position += (Vector3)GridMovement.DirTable[dir];
			}
		}
	}
}
