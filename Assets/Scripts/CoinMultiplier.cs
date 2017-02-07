using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMultiplier : MonoBehaviour {

	float multiplier;

	// Starting multiplier for the coin system
	[Range(0,10)]
	public float MULT_START = 1f;

	// Maximum multiplier
	[Range(0,10)]
	public float MULT_MAX = 3f;

	// Amount to increment by
	[Range(0,1)]
	public float MULT_INTERVAL = 0.5f;

	// Use this for initialization
	void Start () 
	{
		multiplier = MULT_START;
	}
		
	// Adds an interval to the current multiplier
	public void AddMult() 
	{
		if(multiplier < MULT_MAX){
			multiplier += MULT_INTERVAL;
		}
		if(multiplier > MULT_MAX) {
			multiplier = MULT_MAX;
		}
	}

	// Sets multuplier to start. Duh.
	public void Reset() 
	{
		multiplier = MULT_START;
	}

	public float GetMult() { return multiplier; }
}
