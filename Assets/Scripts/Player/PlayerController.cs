using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(EntityAudioController))]
[RequireComponent(typeof(GridMovement))]
public class PlayerController : MonoBehaviour {

	// Gamd updater
	public GameObject body;
	public GameObject head;
	public GameObject updater;


	// Sends messages
	public GameObject weaponSprite;
	// Utility GridMovement component

	public List<AudioClip> jump_sounds;
	[Range(0,1)]
	public float jump_vol;

	GridMovement gm;
	// World simultaneous updater
	SimultaneousUpdater smu;
	// World timer
	WorldTimer wt;
	// Current weapon equipped to the player. Should be added as a component. 
	AbstractWeapon weapon;
	// Controls the coins of the player!
	PlayerCoinController pcc;
	// World coin multiplier. 
	CoinMultiplier wcm;
	// Translator
	GridSpriteTranslate gst;
	// Audio Controller for player. 
	EntityAudioController eac;
	

	// Animator reference
	Animator anim;
	// Brute force thing to keep sprites in sync. 
	Animator anim_head;


	// Use this for initialization
	void Awake () {
		gm = GetComponent<GridMovement>();
		smu = updater.GetComponent<SimultaneousUpdater>();
		wt = updater.GetComponent<WorldTimer>();
		pcc = GetComponent<PlayerCoinController>();
		wcm = updater.GetComponent<CoinMultiplier>();
		gst = GetComponentInChildren<GridSpriteTranslate>();
		anim = body.GetComponent<Animator>();
		anim_head = head.GetComponent<Animator>();
		eac = GetComponent<EntityAudioController>();
	}

	void Start() 
	{
		if(smu == null) {
			Debug.Log("Updater Isn't configured correctly.");
		}
		SetWeapon<DaggerWeapon>();
		//DontDestroyOnLoad(gameObject); //testing
	}
	
	// Update is called once per frame
	void Update () 
	{
		Dirs dir = Dirs.NULL;
		DontDestroyOnLoad(gameObject); //testing

		// // TEST CODE
		// if(Input.GetKeyDown(KeyCode.D)) {
		// 	Debug.Log("Equipped dagger!");
		// 	SetWeapon<DaggerWeapon>();
		// }
		// if(Input.GetKeyDown(KeyCode.S)) {
		// 	Debug.Log("Equipped Longsword!");
		// 	SetWeapon<LongswordWeapon>();
		// }
		// if(Input.GetKeyDown(KeyCode.A)) {
		// 	Debug.Log("Equipped GUN!");
		// 	SetWeapon<GunWeapon>();
		// }
		// // if(Input.GetKeyDown(KeyCode.D)) {
		// // 	GetComponent<PlayerHealthController>().DealDamage(1);
		// // }
		if(Input.GetKeyDown(KeyCode.E)) {
			pcc.AddCoins(20);
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

			// Reset the world timer so it doesn't prematurely fire off
			wt.Reset();

			bool attacked = weapon.Attack(dir); // Attack in the given direction
			if(attacked) {
				eac.PlayAttack();
			}
			// returns false is there was no enemy to attack.
			else {

				if(gm.IsStones(dir)) {
					gst.StartTranslation();

					transform.position += (Vector3)GridMovement.DirTable[dir];

					// NOTE(clark): Testing hopping here. I really like hopping. 
					// gst.SetPosition(GridSpriteTranslate.MoveType.WALK);

					gst.SetPosition(GridSpriteTranslate.MoveType.WALK);
					anim.SetTrigger("Walk");
					anim_head.SetTrigger("RESET");

				}

				// This is an ugly block. Really brute force. OH WELL 
				else if(gm.IsWater(dir)) {

					// Shoot out a ray to check for a collision with the level layer. 
					RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)(GridMovement.DirTable[dir] * 2), 			// origin
														 Vector3.up, 				// Lookup table (direction)!
														 0.1f, 							// Only 1 unit Grid
														 LayerMask.GetMask("Stones")); 	// Only on this layer

			        if(hit.collider != null) {
			        	gst.StartTranslation();

						transform.position += ((Vector3)GridMovement.DirTable[dir] * 2);

						// Play jump noise
						int idx = Random.Range(0, jump_sounds.Count);
						AudioSource.PlayClipAtPoint(jump_sounds[idx], transform.position, jump_vol);

						// NOTE(clark): Testing hopping here. I really like hopping. 
						// gst.SetPosition(GridSpriteTranslate.MoveType.WALK);
						gst.SetPosition(GridSpriteTranslate.MoveType.JUMP);
						anim.SetTrigger("Jump");
						anim_head.SetTrigger("RESET");
			        }
			        else {
			        	// Shoot out a ray to check for a collision with the level layer. 
						hit = Physics2D.Raycast(transform.position + (Vector3)(GridMovement.DirTable[dir] * 2), 			// origin
															 Vector3.up, 				// Lookup table (direction)!
															 0.1f, 							// Only 1 unit Grid
															 LayerMask.GetMask("Water")); 	// Only on this layer
						// NO water
						if(hit.collider == null) {
				        	gst.StartTranslation();

							transform.position += ((Vector3)GridMovement.DirTable[dir] * 2);

							// NOTE(clark): Testing hopping here. I really like hopping. 
							// gst.SetPosition(GridSpriteTranslate.MoveType.WALK);
							gst.SetPosition(GridSpriteTranslate.MoveType.JUMP);
							anim.SetTrigger("Jump");
							anim_head.SetTrigger("RESET");
				        }
			        }
				}

				else if(gm.CanMovePlayer(dir)) {
					
					gst.StartTranslation();

					transform.position += (Vector3)GridMovement.DirTable[dir];

					// NOTE(clark): Testing hopping here. I really like hopping. 
					// gst.SetPosition(GridSpriteTranslate.MoveType.WALK);
					gst.SetPosition(GridSpriteTranslate.MoveType.WALK);
					anim.SetTrigger("Walk");
					anim_head.SetTrigger("RESET");
				}
			}

			// NOTE(clark): Did stuff
			// Fire off a world update
			smu.UpdateWorld();
		}
	}

	// I am being lazy and making the return a string. 
	public string SetWeapon<W>() where W : AbstractWeapon
	{

		// Store what weapon WAS equipped.
		string ret = "null";
		System.Type t;
		if(weapon != null){
			t = weapon.GetType();
			if(t == typeof(DaggerWeapon)) {
				ret = "dagger";
			}
			else if(t == typeof(LongswordWeapon)) {
				ret = "longsword";
			}
			else if(t == typeof(GunWeapon)) {
				ret = "gun";
			}
		}

		// We don't need this weapon anymore. 
		Destroy(weapon);
		weapon = gameObject.AddComponent<W>() as AbstractWeapon;

		// I am being lazy and making the return a string. 

		t = weapon.GetType();
		if(t == typeof(DaggerWeapon)) {
			weaponSprite.SendMessage("EquipWeapon", "dagger");
		}
		else if(t == typeof(LongswordWeapon)) {
			weaponSprite.SendMessage("EquipWeapon", "longsword");
		}
		else if(t == typeof(GunWeapon)) {
			weaponSprite.SendMessage("EquipWeapon", "gun");
		}

		return ret;
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject obj = other.gameObject;
		if(obj.CompareTag("Coins")) {
			CoinComponent coinComponent = obj.GetComponent<CoinComponent>();
			int coins = coinComponent.Coins;
			coins = (int)((float)coins * wcm.GetMult());
			pcc.AddCoins(coins);
			// Play coin pickup sounds.
			coinComponent.PlaySound();

			Destroy(obj);

			GameObject pickup = (GameObject) Instantiate(Resources.Load("CoinPickup"));
			pickup.transform.position = new Vector3(transform.position.x + .75f, transform.position.y, transform.position.z);
			pickup.GetComponent<CoinPickupComponent>().SetCoins(coins);

		}
		else if (obj.CompareTag("Stairs"))
		{
			if (obj.GetComponent<StairController>().IsUnlocked())
			{
				//object.DontDestroyOnLoad(transform.gameObject);
				//DontDestroyOnLoad(PlayerHealthController.gameObject);
				//PlayerPrefs.SetInt(
				Application.LoadLevel("Level2");

				//@Molly: go to the next scene
				//Application.LoadLevel("Level 2");   
			}
		}
	}


}
