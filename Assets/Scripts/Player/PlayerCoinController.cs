using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinController : MonoBehaviour {

	public GameObject UIHundreds;
	public GameObject UITens;
	public GameObject UIOnes;
	Image hundreds;
	Image tens;
	Image ones;

	public int coins;
	public int Coins() {return coins;}

	// Use this for initialization
	void Awake() 
	{
		coins = 0;
		hundreds = UIHundreds.GetComponent<Image>();
		tens = UITens.GetComponent<Image>();
		ones = UIOnes.GetComponent<Image>();
	}

	void Start() 
	{
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
		// hundreds.sprite = 
	}
}
