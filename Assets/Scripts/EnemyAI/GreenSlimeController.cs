using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUpdateSubscriber))]
[RequireComponent(typeof(GridMovement))]
public class GreenSlimeController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GridUpdateSubscriber gus = GetComponent<GridUpdateSubscriber>();
		gus.SetSubscriberMethod(new GridUpdateSubscriber.SubscriberDelegate(SubUpdate));
	}
		
	void SubUpdate() 
	{
		// Debug.Log("Delegate System works!");
	}
}
