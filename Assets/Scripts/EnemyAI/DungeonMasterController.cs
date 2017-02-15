using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovementSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class DungeonMasterController : MonoBehaviour {

	//path and current position in path
	int stepCap = 2;
	[Range(0,1)]
	public int step = 0;

	[Range(1,100)]
	public int maxDist = 11;

	Dirs dir = Dirs.NULL;

	public Animator anim;

	EnemyComponent ec;

	// Utility GridMovement component
	GridMovement gm;

	// Projected movement
	Vector3 projectedMovement;

	// Caching attack because we care!
	bool attacked;

	GameObject player;

	// Use this for initialization
	void Start () 
	{
		Init();
	}

	protected void Init()
	{
		// NOTE(clark, 2/8/2017): Added ec. Calling GetComponent takes a fair bit of time
		ec = GetComponent<EnemyComponent>();
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		GridMovementSubscriber gms = GetComponent<GridMovementSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
		gms.SetMovementMethod(new GridMovementSubscriber.MovementMethod(SubMovement));
		gms.SetAttackMethod(new GridMovementSubscriber.AttackMethod(SubAttack));


		//NOTE TO TJ LOOKY HERE!!!!!! Change to FindWithTag("Player") when player's tag gets set
		player = GameObject.Find("MainPlayer");
	}

	// Subscriber method. 
	void SubUpdate() 
	{
		//puts arms up after going right
		//if he is on step 0 currently, he just put his arms down (do nothing)
		if (step == 0)
			dir = Dirs.NULL;
		if (step == 1)
		{
			dir = Dirs.NULL;
			Vector2 distToPlayer = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			if (distToPlayer.magnitude < maxDist)
			if (distToPlayer.magnitude > .5)
			{
				if (Mathf.Abs(distToPlayer.x) >= Mathf.Abs(distToPlayer.y))
				{
					if (distToPlayer.x >= 0)
						dir = Dirs.RIGHT;
					else
						dir = Dirs.LEFT;
				}
				else if (distToPlayer.y >= 0)
					dir = Dirs.UP;
				else
					dir = Dirs.DOWN;
			}
			else dir = Dirs.NULL;

		}

		if (dir != Dirs.NULL)
		{
			// returns false is there was no enemy to attack.
			if (!attacked) {

				step++;
				step %= stepCap;
				//step++;	
				//step %= dirs.Length;
			}
			// NOTE(clark): If the slime attacked, backtrack to the previous step to keep slime within bounds. 
			else {//NOTE(TJ): I removed this because it makes the gold slime go off pattern. Now the slime just keeps trying if he doesn't get to his new position

				step++;
				step %= stepCap;
				//step += dirs.Length - 1;
				//step %= dirs.Length;
			}
		}
		// NOTE(clark): Added an else to fix update bug. 
		else{

			step++;
			step %= stepCap;
			//step++;	
			//step %= dirs.Length;
		}
	}

	// Movement Subscriber Method
	Vector2 SubMovement() 
	{
		return GridMovement.DirTable[dir];
	}

	// 
	bool SubAttack()
	{
		attacked = ec.Attack(GridMovement.DirTable[dir]); 
		return attacked;	
	}
}
