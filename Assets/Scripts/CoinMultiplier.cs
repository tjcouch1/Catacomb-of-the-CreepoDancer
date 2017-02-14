using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMultiplier : MonoBehaviour {

	float multiplier;

	// Starting multiplier for the coin system

	public GameObject whole;
	public GameObject fract;

	DigitController w_dig;
	DigitController f_dig;

	[Range(0,9)]
	public float MULT_START = 1f;

	// Maximum multiplier
	[Range(0,9)]
	public float MULT_MAX = 3f;

	// Amount to increment by
	[Range(0,1)]
	public float MULT_INTERVAL = 0.5f;

	// Use this for initialization
	void Start () 
	{
		multiplier = MULT_START;
		w_dig = whole.GetComponent<DigitController>();
		f_dig = fract.GetComponent<DigitController>();
		SetGUI();
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

		SetGUI();
	}

	// Sets multuplier to start. Duh.
	public void Reset() 
	{
		multiplier = MULT_START;
		SetGUI();
	}

	void SetGUI() 
	{
		int i_whole = (int)multiplier;
		int i_fract = ((int)(multiplier * 10f) % 10);
		if(w_dig != null) {
			w_dig.DisplayDigit(i_whole);
		}
		if(f_dig != null) {
			f_dig.DisplayDigit(i_fract);
		}

	}

	public float GetMult() { return multiplier; }
}
