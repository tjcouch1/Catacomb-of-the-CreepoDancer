using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void PlayDie() 
	{
		int idx = Random.Range(0, die_sounds.Count);
		AudioSource.PlayClipAtPoint(die_sounds[idx], transform.position, die_vol);
	}

	public void PlayHurt() 
	{
		int idx = Random.Range(0, hurt_sounds.Count);
		AudioSource.PlayClipAtPoint(hurt_sounds[idx], transform.position, hurt_vol);
	}

	public void PlayAttack() 
	{
		int idx = Random.Range(0, attack_sounds.Count);
		AudioSource.PlayClipAtPoint(attack_sounds[idx], transform.position, attack_vol);
	}

}
