using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EntityAudioController : MonoBehaviour {

	public List<AudioClip> hurt_sounds;
	[Range(0,1)]
	public float hurt_vol;

	public List<AudioClip> attack_sounds;
	[Range(0,1)]
	public float attack_vol;

	public List<AudioClip> die_sounds;
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
		int idx = Random.Range(0, die_sounds.Count);
		load_sound(die_sounds[idx], die_vol);
		a_source.Play();
	}

	public void PlayHurt() 
	{
		int idx = Random.Range(0, hurt_sounds.Count);
		load_sound(hurt_sounds[idx], hurt_vol);
		a_source.Play();
	}

	public void PlayAttack() 
	{
		int idx = Random.Range(0, attack_sounds.Count);
		load_sound(attack_sounds[idx], attack_vol);
		a_source.Play();
	}

}
