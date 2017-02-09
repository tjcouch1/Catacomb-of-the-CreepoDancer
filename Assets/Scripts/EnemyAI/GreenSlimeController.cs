using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovementSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class GreenSlimeController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		GridMovementSubscriber gms = GetComponent<GridMovementSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
		gms.SetMovementMethod(new GridMovementSubscriber.MovementMethod(SubMovement));
		gms.SetAttackMethod(new GridMovementSubscriber.AttackMethod(SubAttack));
	}
		
	void SubUpdate() 
	{
		Debug.Log("Delegate System works!");
	}

	Vector2 SubMovement()
	{
		Debug.Log("SubMovement Called");
		return Vector2.zero;
	}

	bool SubAttack() 
	{
		Debug.Log("SubAttack Called");
		return false;
	} 
}
