using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairController : MonoBehaviour {

	bool unlocked = false;

	public string levelString;
	Animator anim;

	EntityAudioController eac;

	void Awake() 
	{
		eac = GetComponent<EntityAudioController>();
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!unlocked && GameObject.FindGameObjectWithTag("DungeonMaster") == null)
		{
			unlocked = true;
			anim.SetBool("Unlocked", unlocked);
			eac.PlayHurt();//unlock sound
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		GameObject obj = other.gameObject;
		if (obj.CompareTag("Player"))
		{
			if (IsUnlocked())
			{
				obj.GetComponent<PlayerController>().StoreInfo();
				SceneManager.LoadScene (levelString);
				// Destroy (gameObject);//added
			}
		}
	}

	public bool IsUnlocked()
	{
		return unlocked;
	}
}
