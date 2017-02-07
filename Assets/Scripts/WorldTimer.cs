using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoinMultiplier))]
[RequireComponent(typeof(SimultaneousUpdater))]
public class WorldTimer : MonoBehaviour {

	[Range(0,2)]
	public float turnTime;

	float current;
	SimultaneousUpdater smu;
	CoinMultiplier cm;

	void Awake() 
	{
		current = 0;
		smu = GetComponent<SimultaneousUpdater>();
		cm = GetComponent<CoinMultiplier>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		current += Time.deltaTime;
		if(current > turnTime) {
			ForceUpdate();
		}
	}

	void ForceUpdate() 
	{
		Reset();
		smu.UpdateWorld();
		cm.Reset();
	}

	public void Reset() 
	{
		current = 0;
	}
}
