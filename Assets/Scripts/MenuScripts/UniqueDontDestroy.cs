using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DontDestroy))]
public class UniqueDontDestroy : MonoBehaviour {
	public string base_scene;
	bool OG = false;

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

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if(scene.name == base_scene) {	
			GameObject[] controllers = GameObject.FindGameObjectsWithTag("BackgroundMusicController");
			if(controllers.Length > 1) {
				if(!OG) {
					Destroy(gameObject);
				}
			}
			else {
				OG = true;
			}
		}
	}
}
