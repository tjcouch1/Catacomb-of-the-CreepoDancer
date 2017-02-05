using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUpdateSubscriber : MonoBehaviour 
{

	public delegate void SubscriberDelegate();
	SubscriberDelegate _subMethod = null;
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SubUpdate() 
	{
		if(_subMethod != null) {
			_subMethod();
		}
	}

	public void SetSubscriberMethod(SubscriberDelegate method) 
	{
		_subMethod = method;
	}

}
