using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

public class EnemyComponent : MonoBehaviour {

	[Range(0,100)]
	public int goldDrop;

	[Range(0,10)]
	public int maxHealth;

	[Range(0,10)]
	public int attackPower;
	int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	

	public void Hit(int dmg) {
		health -= dmg;
		if(health <= 0) {
			Die();
		}
	}

	public void Die()
	{
		//add coins to player
		Destroy(gameObject);
	}

	public bool Attack(Vector2 offset)
	{
		// Check the grid position for the player. 
		RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)offset, 	// origin
											 Vector2.up, 			// Lookup table (direction)!
											 0.1f, 									// Only 1 unit Grid
											 LayerMask.GetMask("Player")); 			// Only on this layer

		// If a collider exists, we found an enemy
		if(hit.collider != null){
			// Debug.Log("FOUND AN ENEMY!!!");
			PlayerHealthController phc = hit.collider.gameObject.GetComponent<PlayerHealthController>();
			if(phc != null) {
				phc.DealDamage(attackPower);
			}
		}
		return (hit.collider != null);
	}
}
