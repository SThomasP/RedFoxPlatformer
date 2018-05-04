using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

	public float fadeTime;
	float timeFaded;
	float full = 1.0f;
	float appearing = 0.0f;
	GameObject player;
	SpriteRenderer[] sprites;
	// Use this for initialization
	void Start () {
		timeFaded = fadeTime;
		player = GameObject.Find("player");
		sprites = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in sprites) {
			sr.color = new Color (full, full, full, 0.0f);
		}
	}

	void Update(){
		if (fadeTime > timeFaded) {
			foreach (SpriteRenderer sr in sprites){
				float fadeAmount = Mathf.Abs(appearing - timeFaded / fadeTime);
				sr.color = new Color(full, full, full, fadeAmount);
			}
			timeFaded = timeFaded += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player){
			timeFaded = 0.0f;
			appearing = 0.0f;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player){
			timeFaded = 0.0f;
			appearing = 1.0f;
		}
	}
}
