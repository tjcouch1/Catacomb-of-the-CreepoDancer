using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovementSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class BlueSlimeController : MonoBehaviour {

	//path and current position in path
	int step = 0;
	protected Dirs[] dirs = { Dirs.DOWN, Dirs.NULL, Dirs.UP, Dirs.NULL };

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
			else {//NOTE(TJ): I removed this because it makes the gold slime go off pattern. Now the slime just keeps trying if he doesn't get to his new position
				
				//step += dirs.Length - 1;
				//step %= dirs.Length;
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
