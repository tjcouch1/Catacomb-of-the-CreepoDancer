using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EntityAudioController : MonoBehaviour {

	public AudioClip hurt_sound;
	[Range(0,1)]
	public float hurt_vol;

	public AudioClip attack_sound;
	[Range(0,1)]
	public float attack_vol;

	public AudioClip die_sound;
	[Range(0,1)]
	public float die_vol;
	
	AudioSource a_source;


	void Awake() 
	{
		a_source = GetComponent<AudioSource>();
	}

	void load_sound(AudioClip ac, float volume) 
	{
		a_source.Stop();
		a_source.clip = ac;
		a_source.volume = volume;
	}

	public void PlayDie() 
	{
		load_sound(die_sound, die_vol);
		a_source.Play();
	}

	public void PlayHurt() 
	{
		load_sound(hurt_sound, hurt_vol);
		a_source.Play();
	}

	public void PlayAttack() 
	{
		load_sound(attack_sound, attack_vol);
		a_source.Play();
	}

}
