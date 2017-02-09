using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class BlueSlimeController : MonoBehaviour {

	//path and current position in path
	int step = 0;
	Dirs[] dirs = { Dirs.DOWN, Dirs.NULL, Dirs.UP, Dirs.NULL };

	EnemyComponent ec;

	// Utility GridMovement component
	GridMovement gm;

	// Use this for initialization
	void Start () 
	{
		gm = GetComponent<GridMovement>();
		// NOTE(clark, 2/8/2017): Added ec. Calling GetComponent takes a fair bit of time
		ec = GetComponent<EnemyComponent>();
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
	}

	void SubUpdate() 
	{
		if (dirs[step] != Dirs.NULL)
		{
			// NOTE(clark): Keeping references to components helps a lot. 
			bool attacked = ec.Attack(dirs[step]); 
			// bool attacked = gameObject.GetComponent<EnemyComponent>().Attack(dirs[step]); // Attack in the given direction

			// returns false is there was no enemy to attack.
			if (!attacked) {
				if (gm.CanMoveEnemy(dirs[step])) {
					GridSpriteTranslate gst = GetComponentInChildren<GridSpriteTranslate>();
					gst.StartTranslation();
					transform.position += (Vector3) GridMovement.DirTable[dirs[step]];
				}
				step++;	
				step %= dirs.Length;
			}
		}
		// NOTE(clark): Added an else to fix update bug. 
		else{
			step++;	
			step %= dirs.Length;
		}
	}
}
