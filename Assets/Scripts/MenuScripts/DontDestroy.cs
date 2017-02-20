using System.Collections;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
	void Awake()
	{
		DontDestroyOnLoad (gameObject);	
	
		//added
	//if (FindObjectsOfType(GetType()).Length>1)
	//{ 
		//Destroy(gameObject);
	//}
	}
 
}