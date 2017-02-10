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

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image>();
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
