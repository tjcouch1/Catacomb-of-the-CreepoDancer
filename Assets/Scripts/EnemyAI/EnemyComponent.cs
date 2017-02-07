using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour {

	[Range(0,100)]
	public int goldDrop;

	[Range(0,10)]
	public int maxHealth;

	[Range(0,10)]
	public int attackPower;
	int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit(int dmg) {
		health -= dmg;
		if(health <= 0) {
			Destroy(gameObject);
		}
	}
}
