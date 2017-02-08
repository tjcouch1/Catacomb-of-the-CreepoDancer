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

	// Utility GridMovement component
	GridMovement gm;

	// Use this for initialization
	void Start () 
	{
		gm = GetComponent<GridMovement>();
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
	}

	void SubUpdate() 
	{
		if (dirs[step] != Dirs.NULL)
		{
			bool attacked = gameObject.GetComponent<EnemyComponent>().Attack(dirs[step]); // Attack in the given direction

			// returns false is there was no enemy to attack.
			if (!attacked)
				if (gm.CanMove(dirs[step]))
					transform.position += (Vector3) GridMovement.DirTable[dirs[step]];
		}
		step++;
		step %= dirs.Length;
		Debug.Log("Delegate System works!");
	}
}
