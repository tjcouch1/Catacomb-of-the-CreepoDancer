using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoinMultiplier))]
[RequireComponent(typeof(SimultaneousUpdater))]
public class WorldTimer : MonoBehaviour {

	public GameObject timerBar;

	[Range(0,2)]
	public float turnTime;
	BasicImageScalar scl;

	float current;
	SimultaneousUpdater smu;
	CoinMultiplier cm;

	void Awake() 
	{
		current = 0;
		smu = GetComponent<SimultaneousUpdater>();
		cm = GetComponent<CoinMultiplier>();
		scl = timerBar.GetComponent<BasicImageScalar>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Update time
		current += Time.deltaTime;

		// Update UI
		scl.SetXScale((turnTime - current) / turnTime);

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
