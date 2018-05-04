using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {


	bool playerAtDoor = false;
	GameObject player;

	public int sceneIndex;

	GameObject exit;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player) {
			playerAtDoor = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player) {
			playerAtDoor = false;
		}
	}


	// Update is called once per frame
	void Update () {
		bool disable = player.GetComponent<PlayerScript> ().disabled;
		if (Input.GetButtonDown ("Use") && playerAtDoor && ! disable) {
			SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
		}
	}


}
