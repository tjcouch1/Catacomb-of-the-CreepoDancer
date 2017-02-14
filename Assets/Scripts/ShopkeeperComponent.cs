using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperComponent : MonoBehaviour {

	public GameObject thank_prefab;
	GameObject thank;

	public void Thank() 
	{
		thank = (GameObject)Instantiate(thank_prefab);
		thank.transform.position = transform.position + new Vector3(1.0f, 0.9f, 0);
		Invoke("Remove", 2f);
	}

	void Remove() 
	{
		Destroy(thank);
	}
}
