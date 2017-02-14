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
		GameObject coins = (GameObject) Instantiate(Resources.Load("CoinDrop"));
		// position
		coins.transform.position = transform.position;
		// coins
		coins.GetComponent<CoinComponent>().Coins = goldDrop;
		// boom
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

				// Direction
				Vector2 vdir = offset;

				// Make an arrow.
				GameObject swipe = Instantiate(Resources.Load("EnemySwipe") as GameObject);

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
