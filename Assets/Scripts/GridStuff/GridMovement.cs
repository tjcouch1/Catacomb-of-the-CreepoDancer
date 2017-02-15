using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public enum Directions 
	{
		NULL,
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	public static Dictionary<Directions, Vector2> DirTable = new Dictionary<Directions, Vector2> 
	{
		{Directions.NULL, Vector2.zero},
		{Directions.UP, Vector2.up},
		{Directions.DOWN, -Vector2.up},
		{Directions.RIGHT, Vector2.right},
		{Directions.LEFT, -Vector2.right},
	};

	
	public bool CanMovePlayer(Directions dir) 
	{
		return CanMove(dir, LayerMask.GetMask("Level", "Enemies"));
	}

	public bool IsWater(Directions dir) 
	{
		return CanMove(dir, LayerMask.GetMask("Water"));
	}

	public bool CanMoveEnemy(Directions dir) 
	{
		return CanMove(dir, LayerMask.GetMask("Level", "Player"));
	}

	bool CanMove(Directions dir, LayerMask lay) 
	{
		if(dir == Directions.NULL) {
			return false;
		}
		// Shoot out a ray to check for a collision with the level layer. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)DirTable[dir], 			// origin
											 Vector3.up, 				// Lookup table (direction)!
											 0.1f, 							// Only 1 unit Grid
											 lay); 	// Only on this layer

		// If null, no collision! Yay!
        return (hit.collider == null);
	}
}
