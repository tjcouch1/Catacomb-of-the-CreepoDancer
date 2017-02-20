using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateStorage : MonoBehaviour {


	public int coins = 0;
	public string weapon = "dagger";
	public int health = 0;

	public void StoreInfo(int c, string w, int h)
	{
		coins = c;
		weapon = w;
		health = h;
	}

	public int GetCoins() { return coins; }
	public string GetWeapon() { return weapon; }
	public int GetHealth() { return health; }
}
