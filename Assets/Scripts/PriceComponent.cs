using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceComponent : MonoBehaviour {

	[Range(0,999)]
	public int price;

	public GameObject shopkeep;
	public GameObject hund;
	public GameObject tens;
	public GameObject ones;

	// Use this for initialization
	void Start () {

		// hundreds.DisplayDigit(coins / 100);
		// tens.DisplayDigit((coins % 100) / 10);
		// ones.DisplayDigit(coins % 10);
		hund.GetComponent<GameDigitController>().DisplayDigit(price / 100);
		tens.GetComponent<GameDigitController>().DisplayDigit((price % 100) / 10);
		ones.GetComponent<GameDigitController>().DisplayDigit(price % 10);

	}
	
	public int GetPrice() { return price; }

	public void SayThanks() 
	{
		shopkeep.GetComponent<ShopkeeperComponent>().Thank();
	}

}
