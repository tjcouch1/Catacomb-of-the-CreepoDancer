using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public enum Directions {
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	public static Dictionary<Directions, Vector2> DirTable = new Dictionary<Directions, Vector2> {
		{Directions.UP, Vector2.up},
		{Directions.DOWN, -Vector2.up},
		{Directions.RIGHT, Vector2.right},
		{Directions.LEFT, -Vector2.right},
	};

	
	public bool CanMove(Directions dir) {
		// Shoot out a ray to check for a collision with the level layer. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, 			// origin
											 DirTable[dir], 				// Lookup table (direction)!
											 1f, 							// Only 1 unit Grid
											 LayerMask.GetMask("Level")); 	// Only on this layer

		// If null, no collision! Yay!
        return (hit.collider == null);
	}
}
