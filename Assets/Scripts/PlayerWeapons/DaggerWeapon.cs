using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

public class DaggerWeapon : AbstractWeapon {

	// Use this for initialization
	public override bool Attack(Dirs dir) 
	{
		// Shoot out a ray to check for a collision with the level layer. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, 			// origin
											 GridMovement.DirTable[dir], 	// Lookup table (direction)!
											 1f, 							// Only 1 unit Grid
											 LayerMask.GetMask("Enemies")); // Only on this layer

		// If a collider exists, we found an enemy
		if(hit.collider != null){
			Debug.Log("FOUND AN ENEMY!!!");
		}
		return (hit.collider != null);
	}
	
	// // Update is called once per frame
	// public override bool HasTarget(Dirs dir) 
	// {
	// 	// Shoot out a ray to check for a collision with the level layer. 
	// 	RaycastHit2D hit = Physics2D.Raycast(transform.position, 			// origin
	// 										 GridMovement.DirTable[dir], 	// Lookup table (direction)!
	// 										 1f, 							// Only 1 unit Grid
	// 										 LayerMask.GetMask("Enemies")); // Only on this layer

	// 	// If a collider exists, we found an enemy
	// 	if(hit.collider != null){
	// 		Debug.Log("FOUND AN ENEMY!!!");
	// 	}
	// 	return null;
	// }
}
