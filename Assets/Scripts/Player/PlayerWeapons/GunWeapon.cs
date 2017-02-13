using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

public class GunWeapon : AbstractWeapon {

	float range = 8f;
	float speed = 20f;

	public override bool Attack(Dirs dir) 
	{
		// Shoot out a ray to check for a collision with the level layer. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, 			// origin
											 GridMovement.DirTable[dir], 	// Lookup table (direction)!
											 range, 								// Only 1 unit Grid
											 LayerMask.GetMask("Enemies", "Level")); // Only on this layer

		// If hit, check layer
		if(hit.collider != null) {
			GameObject obj = hit.collider.gameObject;
			LayerMask mask = obj.layer;

			// Layer stuff
			if(mask == LayerMask.NameToLayer("Layer")) {
				return false;
			}

			// Enemies and stuff
			if(mask == LayerMask.NameToLayer("Enemies")) {
				Vector2 vdir = GridMovement.DirTable[dir];

				// Make an arrow.
				GameObject arrow = Instantiate(Resources.Load("Arrowthing") as GameObject);
				arrow.GetComponent<Rigidbody2D>().velocity = ((Vector3)vdir) * speed;

				// Set potision 
				Transform trans = arrow.transform;
				trans.position = transform.position;	

				// Rotate
				float angle = Mathf.Atan2(vdir.y,vdir.x) * Mathf.Rad2Deg;
 				trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


				// Make a swipe.
				GameObject swipe = Instantiate(Resources.Load("ArrowSwipe") as GameObject);

				// Set potision 
				trans = swipe.transform;
				trans.position = transform.position + ((Vector3)vdir * 1f);	

				// Rotate
				angle = Mathf.Atan2(vdir.y,vdir.x) * Mathf.Rad2Deg;
 				trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);		

 				return true;	
			}

		}

		return false;
	}
	
}
