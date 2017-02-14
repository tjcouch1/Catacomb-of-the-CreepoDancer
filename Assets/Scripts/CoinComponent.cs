using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComponent : MonoBehaviour {

	[Range(1, 100)]
	public int coins = 1;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger("Coins", coins);
	}

	public int Coins {
		get { return coins; }
		set { coins = value; }
	}
}
