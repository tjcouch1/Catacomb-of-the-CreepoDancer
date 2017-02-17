using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDigitController : MonoBehaviour {

	// Assumed to be sequential for simplicity.
	public int starting_digit;
	public List<Sprite> digit_sprites;
	Dictionary<int, Sprite> sprite_dict;
	SpriteRenderer img;

	void Awake() 
	{
		img = GetComponent<SpriteRenderer>();

		sprite_dict = new Dictionary<int, Sprite>();
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