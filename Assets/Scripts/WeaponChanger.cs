using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour {

	public Sprite daggerImage;
	public Sprite longswordImage;
	public Sprite gunImage;

	Image img;

	void Awake() 
	{
		img = GetComponent<Image>();
	}
	
	void EquipWeapon(string weapon) 
	{
		if(weapon == "dagger") {
			img.sprite = daggerImage;
		}
		else if(weapon == "longsword") {
			img.sprite = longswordImage;
		}
		else if(weapon == "gun"){
			img.sprite = gunImage;
		}
		else {
			// img.sprite = 
		}
	}
}
