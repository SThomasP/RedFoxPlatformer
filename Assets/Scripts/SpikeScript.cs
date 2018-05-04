using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "player")
		{
			theCollision.gameObject.GetComponent<PlayerScript> ().Die();

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
