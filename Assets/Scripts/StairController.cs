using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour {

	bool unlocked = false;

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

	public bool IsUnlocked()
	{
		return unlocked;
	}
}
