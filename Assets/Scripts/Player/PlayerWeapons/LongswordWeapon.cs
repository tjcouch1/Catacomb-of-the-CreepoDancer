using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

public class LongswordWeapon : AbstractWeapon {

	int damage = 1;

	// Use this for initialization
	public override bool Attack(Dirs dir) 
	{
		// Shoot out a ray to check for a collision with the level layer. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)GridMovement.DirTable[dir], 			// origin
											 Vector2.up, 	// Lookup table (direction)!
											 0.1f, 							// Only 1 unit Grid
											 LayerMask.GetMask("Enemies")); // Only on this layer


		RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (2 * (Vector3)GridMovement.DirTable[dir]), 			// origin
											 Vector2.up, 	// Lookup table (direction)!
											 0.1f, 							// Only 1 unit Grid
											 LayerMask.GetMask("Enemies")); // Only on this layer

		// If a collider exists, we found an enemy
		if(hit.collider != null){
			// Debug.Log("FOUND AN ENEMY!!!");
			EnemyComponent ec = hit.collider.gameObject.GetComponent<EnemyComponent>();
			if(ec != null) {
				ec.Hit(damage);
			}
		}

		if(hit2.collider != null){
			// Debug.Log("FOUND AN ENEMY!!!");
			EnemyComponent ec = hit2.collider.gameObject.GetComponent<EnemyComponent>();
			if(ec != null) {
				ec.Hit(damage);
			}
		}

		return (hit.collider != null || hit2.collider != null);
	}
	
}
