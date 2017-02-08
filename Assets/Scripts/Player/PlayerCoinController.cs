using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinController : MonoBehaviour {

	int coins;
	public GameObject UIHundreds;
	public GameObject UITens;
	public GameObject UIOnes;
	DigitController hundreds;
	DigitController tens;
	DigitController ones;

	// Brute force implementation. 

	public int Coins() {return coins;}

	// Use this for initialization
	void Awake() 
	{
		coins = 0;
		hundreds = UIHundreds.GetComponent<DigitController>();
		tens = UITens.GetComponent<DigitController>();
		ones = UIOnes.GetComponent<DigitController>();
	}

	void Start() 
	{
		changeUI();
	}
	
	public void AddCoins(int c) 
	{
		coins += c;
		if(coins > 999) {
			coins = 999;
		}
		changeUI();
	}

	public void RemoveCoins(int c) 
	{
		coins -= c;
		if(coins < 0) {
			coins = 0;
		}
		changeUI();
	}

	void changeUI() {
		hundreds.DisplayDigit(coins / 100);
		tens.DisplayDigit((coins % 100) / 10);
		ones.DisplayDigit(coins % 10);
	}
}
