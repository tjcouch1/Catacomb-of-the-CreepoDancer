using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dirs = GridMovement.Directions;

// abstract class for weapons to inherit from!
public abstract class AbstractWeapon : MonoBehaviour {
	public abstract bool Attack(Dirs dir); // Attacks in a direction. False if no enemy.  
}
