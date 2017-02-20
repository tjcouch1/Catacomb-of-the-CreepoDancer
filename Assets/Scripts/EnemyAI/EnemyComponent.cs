using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(EntityAudioController))]
public class EnemyComponent : MonoBehaviour {

	bool dead = false;

	[Range(0,100)]
	public int goldDrop;

	[Range(0,10)]
	public int maxHealth;

	[Range(0,10)]
	public int attackPower;
	int health;

	EntityAudioController eac;

	void Awake() 
	{
		eac = GetComponent<EntityAudioController>();
	}

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	

	public void Hit(int dmg) {
		health -= dmg;
		if(health <= 0) {
			Die();
		}
		else {
			// Hurt sound
			eac.PlayHurt();
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

		dead = true;
		// Die sound
		eac.PlayDie();
		
		// boom
		Destroy(gameObject);

		// Increment multiplier. 
		GameObject controller = GameObject.FindGameObjectsWithTag("Controller")[0];
		if(controller != null) {
			CoinMultiplier cm = controller.GetComponent<CoinMultiplier>();
			cm.AddMult();
		}
	}

	public bool Attack(Vector2 offset)
	{
		if(!dead){
			// Attack sound
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
					eac.PlayAttack();

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
		else {
			return false;
		}
	}

	public bool AttackShoot(Dirs dir, float dist, GameObject player)
	{
		if(!dead){
			if (dir == Dirs.UP || dir == Dirs.DOWN || player.transform.position.y != transform.position.y)
				return Attack(GridMovement.DirTable[dir]);
			// Shoot out a ray to check for a collision with the level layer. 
			RaycastHit2D hit = Physics2D.Raycast(transform.position, 			// origin
				GridMovement.DirTable[dir], 	// Lookup table (direction)!
				7f, 								// range
				LayerMask.GetMask("Player", "Level")); // Only on this layer

			// If hit, check layer
			if(hit.collider != null) {
				GameObject obj = hit.collider.gameObject;
				LayerMask mask = obj.layer;

				// Layer stuff
				if(mask == LayerMask.NameToLayer("Layer")) {
					return false;
				}

				Vector2 vdir = GridMovement.DirTable[dir];

				// Make an arrow.
				GameObject arrow = Instantiate(Resources.Load("Arrowthing") as GameObject);
				arrow.GetComponent<Rigidbody2D>().velocity = ((Vector3) vdir) * 20f;//speed
				arrow.GetComponent<ArrowScript>().SetLMask("Player");
				arrow.GetComponent<ArrowScript>().damage = 2;

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
			}

			return hit.collider != null;
		}
		else {
			return false;
		}
	}
}