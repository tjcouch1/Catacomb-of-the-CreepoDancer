using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovementSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class GoldSlimeController : BlueSlimeController {

	Dirs[] newDirs = { Dirs.DOWN, Dirs.LEFT, Dirs.UP, Dirs.RIGHT };

	void Start()
	{
		base.Init();
		dirs = newDirs;
	}
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovementSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class GoldSlimeController : MonoBehaviour {

	//path and current position in path
	int step = 0;
	Dirs[] dirs = { Dirs.DOWN, Dirs.LEFT, Dirs.UP, Dirs.RIGHT };

	EnemyComponent ec;

	// Utility GridMovement component
	GridMovement gm;

	// Projected movement
	Vector3 projectedMovement;

	// Caching attack because we care!
	bool attacked;

	// Use this for initialization
	void Start () 
	{
		// NOTE(clark, 2/8/2017): Added ec. Calling GetComponent takes a fair bit of time
		ec = GetComponent<EnemyComponent>();
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		GridMovementSubscriber gms = GetComponent<GridMovementSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
		gms.SetMovementMethod(new GridMovementSubscriber.MovementMethod(SubMovement));
		gms.SetAttackMethod(new GridMovementSubscriber.AttackMethod(SubAttack));
	}

	// Subscriber method. 
	void SubUpdate() 
	{
		if (dirs[step] != Dirs.NULL)
		{
			// returns false is there was no enemy to attack.
			if (!attacked) {

				step++;	
				step %= dirs.Length;
			}
			// NOTE(clark): If the slime attacked, backtrack to the previous step to keep slime within bounds. 
			else {
				step += dirs.Length - 1;
				step %= dirs.Length;
			}
		}
		// NOTE(clark): Added an else to fix update bug. 
		else{
			step++;	
			step %= dirs.Length;
		}
	}

	// Movement Subscriber Method
	Vector2 SubMovement() 
	{
		return GridMovement.DirTable[dirs[step]];
	}

	// 
	bool SubAttack()
	{
		attacked = ec.Attack(GridMovement.DirTable[(dirs[step])]); 
		return attacked;	
	}
}
*/