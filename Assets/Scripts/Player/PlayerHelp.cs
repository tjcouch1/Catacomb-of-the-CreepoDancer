using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelp : MonoBehaviour {

	public GameObject move;
	public GameObject attack;
	public GameObject buy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move != null)
			if (!transform.position.Equals(move.transform.position))
				Destroy(move);
		if (attack != null)
			if (transform.position.Equals(attack.transform.position))
				Destroy(attack);
		if (buy != null)
			if (transform.position.Equals(buy.transform.position))
				Destroy(buy);
	}
}
