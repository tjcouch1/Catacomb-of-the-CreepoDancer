using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateStorage : MonoBehaviour {


	int coins = 0;
	string weapon = "dagger";
	int health = 0;

	public void StoreInfo(int c, string w, int h)
	{
		coins = c;
		weapon = w;
		health = h;
	}

	public int GetCoins() { return coins; }
	public string GetWeapon() { return weapon; }
	public int GetHealth() { return health; }

	void OnEnable()
	{
	//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;	
	}

	void reset() 
	{
		coins = 0;
		weapon = "dagger";
		health = 999;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		if(scene.name == "Level1") {	
			reset();
		}
	}
}
