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
	//start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		// stop listening for a scene change as soon as this script is disabled. 
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
