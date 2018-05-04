using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip jumpSound;
	public static AudioClip superJumpSound;
	public static AudioClip walkSound;
	public static AudioClip deathSound;
	static AudioSource audioSource;

	// Use this for initialization
	void Start () {
		jumpSound = Resources.Load<AudioClip> ("Sounds/RetroGamesSoundFX/Alert/Alert01");
		superJumpSound = Resources.Load<AudioClip> ("Sounds/RetroGamesSoundFX/Various/Various09");
		deathSound = Resources.Load<AudioClip> ("Sounds/RetroGamesSoundFX/Hit/Hit8");


		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound(string sound) {
		switch (sound) {
		case "jump":
			audioSource.PlayOneShot (jumpSound);
			break;
		case "super jump":
			audioSource.PlayOneShot (superJumpSound);
			break;
		case "death":
			audioSource.PlayOneShot (deathSound);
			break;
		default:
			break;
		}
	}
}
