﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridMovement))]
public class PlayerController : MonoBehaviour {

	// Gamd updater
	public GameObject updater;
	// Utility GridMovement component
	GridMovement gm;
	// World simultaneous updater
	SimultaneousUpdater smu;
	// World timer
	WorldTimer wt;
	// Current weapon equipped to the player. Should be added as a component. 
	AbstractWeapon weapon;

	// Use this for initialization
	void Awake () {
		gm = GetComponent<GridMovement>();
		smu = updater.GetComponent<SimultaneousUpdater>();
		wt = updater.GetComponent<WorldTimer>();
		SetWeapon<DaggerWeapon>();
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
		Dirs dir = Dirs.NULL;


		// TEST CODE
		// if(Input.GetKeyDown(KeyCode.D)) {
		// 	Debug.Log("Equipped dagger!");
		// 	SetWeapon<DaggerWeapon>();
		// }
		// if(Input.GetKeyDown(KeyCode.S)) {
		// 	Debug.Log("Equipped Longsword!");
		// 	SetWeapon<LongswordWeapon>();
		// }
		if(Input.GetKeyDown(KeyCode.D)) {
			GetComponent<PlayerHealthController>().DealDamage(1);
		}

		// Rightwards Movement
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			dir = Dirs.RIGHT;
		}

		// Leftwards Movement
		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			dir = Dirs.LEFT;
		}

		// Upwards Movement
		else if(Input.GetKeyDown(KeyCode.UpArrow)){
			dir = Dirs.UP;
		}

		// Downwards Movement
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			dir = Dirs.DOWN;
		}

		if(dir != Dirs.NULL) {

			// Fire off a world update
			smu.UpdateWorld();

			// Reset the world timer so it doesn't prematurely fire off
			wt.Reset();

			bool attacked = weapon.Attack(dir); // Attack in the given direction

			// returns false is there was no enemy to attack.
			if(!attacked){
				if(gm.CanMove(dir)) {
					transform.position += (Vector3)GridMovement.DirTable[dir];
				}
			}
			
		}
	}

	public void SetWeapon<W>() where W : AbstractWeapon
	{
		// We don't need this weapon anymore. 
		Destroy(weapon);
		weapon = gameObject.AddComponent<W>() as AbstractWeapon;
	}


}