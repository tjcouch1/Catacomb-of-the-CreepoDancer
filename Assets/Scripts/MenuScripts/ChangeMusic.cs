using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusic : MonoBehaviour {
	public AudioClip level1Music;
	private AudioSource source;	
	bool OG = false;


	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource> ();
	}
	// void OnLevelWasLoaded(int level){
	// 	if (level == 1) {
	// 		source.clip = level1Music;
	// 		source.Play();
	// 	}
	// }

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
		Debug.Log("Level Loaded");
		if(scene.name == "MainMenu") {	
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
		else if(scene.name == "Level1") {
			source.clip = level1Music;
			source.Play();
		}
		// Debug.Log(scene.name);
		// Debug.Log(mode);
	}
}