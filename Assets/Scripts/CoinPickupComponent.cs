using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupComponent : MonoBehaviour {

	public int coins;

	public float lifeTimeCap = .75f;
	float lifeTime;

	public GameObject hund;
	public GameObject tens;
	public GameObject ones;

	// Use this for initialization
	void Start () {
		lifeTime = lifeTimeCap;

		SetCoins(coins);
	}

	public void SetCoins(int c)
	{
		coins = c;
		//Debug.Log(coins/100);
		hund.GetComponent<GameDigitController>().DisplayDigit(coins / 100);
		//Debug.Log((coins % 100) / 10);
		tens.GetComponent<GameDigitController>().DisplayDigit((coins % 100) / 10);
		//Debug.Log(coins % 10);
		ones.GetComponent<GameDigitController>().DisplayDigit(coins % 10);
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 3 / 4, transform.position.z);

		lifeTime -= Time.deltaTime;

		foreach (Transform child in transform)
			child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1 - (lifeTimeCap - lifeTime)) / lifeTimeCap);

		if (lifeTime <= 0)
			Die();
	}

	void Die()
	{
		foreach (Transform child in transform)
			GameObject.Destroy(gameObject);
		Destroy(gameObject);
	}

	public int GetCoins() { return coins; }


}
