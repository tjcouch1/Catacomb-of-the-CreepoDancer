using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	int damage = 1;

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame
	// void Update () {
		// 
	// }

	void OnTriggerEnter2D(Collider2D other) 
	{
		GameObject obj = other.gameObject;

		if(obj.layer == LayerMask.NameToLayer("Enemies")) {
			obj.GetComponent<EnemyComponent>().Hit(damage);
		}	

		Destroy(gameObject);
		// Debug.Log("Arrow hit the thing");
	}
}
