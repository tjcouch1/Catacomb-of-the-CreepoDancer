using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	public int damage = 1;

	string lMask = "Enemies";

	// Use this for initialization
	// void Start () {
		//
	// }
	
	// Update is called once per frame
	// void Update () {
		// 
	// }

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject obj = other.gameObject;

		if (obj.layer == LayerMask.NameToLayer("Level"))
			Destroy(gameObject);
		
		if(obj.layer == LayerMask.NameToLayer(lMask)) 
		{
			if (lMask == "Enemies")
			{
				obj.GetComponent<EnemyComponent>().Hit(damage);

				Destroy(gameObject);
			}
			else if (lMask == "Player")
			{
				PlayerHealthController phc = obj.GetComponent<PlayerHealthController>();
				if(phc != null) {
					phc.DealDamage(damage);
				}

				Destroy(gameObject);
			}
		}
	}

	public void SetLMask(string s)
	{
		lMask = s;
	}
}
