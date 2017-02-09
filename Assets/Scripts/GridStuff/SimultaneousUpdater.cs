using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoveStates = GridMovementSubscriber.MoveStates;

public class SimultaneousUpdater : MonoBehaviour 
{

	public void UpdateWorld() 
	{
		// Get th egus
		GridUpdateSubscriber gus;
		GridMovementSubscriber gms;

		// List of tenative movement objects that couldn't process. 

		// Process enemy movement. 
		foreach (Transform child in transform){
   			gms = child.gameObject.GetComponent<GridMovementSubscriber>();

   			// Process movement for component.
   			if(gms != null) {
   				if(gms.GetState() == MoveStates.NULL) {
	   				gms.Move();
	   			}
   			}
    	}

    	// Process Updates. 
		foreach (Transform child in transform){
   			gus = child.gameObject.GetComponent<GridUpdateSubscriber>();
   			gms = child.gameObject.GetComponent<GridMovementSubscriber>();

   			// Process movement for component.
   			if(gus != null) {
   				gus.SubUpdate();
   			}

   			if(gms != null) {
   				gms.SetState(MoveStates.NULL);
   			}
    	}
	}
}
