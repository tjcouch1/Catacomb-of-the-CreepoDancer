using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimultaneousUpdater : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void UpdateWorld() 
	{
		// Get th egus
		GridUpdateSubscriber gus;

		// For each object (enemy) that's a child of the world
		foreach (Transform child in transform){
			// Get the gus!
   			gus = child.gameObject.GetComponent<GridUpdateSubscriber>();

   			if(gus != null) {
   				// If it exists, update it!
   				gus.SubUpdate();
   			}
    	}
	}
}
