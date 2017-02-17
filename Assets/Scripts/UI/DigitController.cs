using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitController : MonoBehaviour {

	// Assumed to be sequential for simplicity.
	public int starting_digit;
	public List<Sprite> digit_sprites;
	Dictionary<int, Sprite> sprite_dict;
	Image img;

	void Awake() {
		sprite_dict = new Dictionary<int, Sprite>();

		img = GetComponent<Image>();
		int i = 0;
		foreach(Sprite spr in digit_sprites) {
			sprite_dict[i++] = spr;
		}
		img.sprite = sprite_dict[starting_digit];
	}

	public void DisplayDigit(int dig) 
	{	
		img.sprite = sprite_dict[dig];
	}
}
