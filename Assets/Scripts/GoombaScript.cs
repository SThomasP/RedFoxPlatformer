using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour {

	public float speed = 20;

	private Collider2D coll; 
	private Animator anim;
	private SpriteRenderer sr;
	private Rigidbody2D rb2d;
	private Transform trans;

	bool facingRight = false;
	int direction = -1;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
		trans = GetComponent<Transform> ();
	}

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "Walls")
		{
			if (theCollision.contacts [0].point.x < trans.position.x) {
				facingRight = true;
				direction = 1;
				sr.flipX = true;
				Debug.Log ("ow possum hath hit left wall :c");
			} else {
				facingRight = false;
				direction = -1;
				sr.flipX = false;
				Debug.Log ("ow possum hath hit right wall :c");
			}

		}

		// TODO send death
		if (theCollision.gameObject.name == "player")
		{
			theCollision.gameObject.GetComponent<PlayerScript> ().Die();

		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move
		transform.Translate (Vector2.right * direction * Time.deltaTime * speed);
	}
}
