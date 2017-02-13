using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

public class DaggerWeapon : AbstractWeapon {

	int damage = 1;

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
			// Debug.Log("FOUND AN ENEMY!!!");
			EnemyComponent ec = hit.collider.gameObject.GetComponent<EnemyComponent>();
			if(ec != null) {
				ec.Hit(damage);

				// Direction
				Vector2 vdir = GridMovement.DirTable[dir];

				// Make an arrow.
				GameObject swipe = Instantiate(Resources.Load("DaggerSwipe") as GameObject);

				// Set potision 
				Transform trans = swipe.transform;
				trans.position = transform.position + (Vector3)vdir;	

				// Rotate
				float angle = Mathf.Atan2(vdir.y,vdir.x) * Mathf.Rad2Deg;
 				trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);	
			}
		}
		return (hit.collider != null);
	}
	
}
