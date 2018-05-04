using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float xfactor;
	public float yfactor;
	public float smooth;

	private Rigidbody2D rb2d;
	private Transform transform;
	private Vector3 velocity = new Vector3 (0f, 0f, 0f);

	// Use this for initialization
	void Start () {
		rb2d = GameObject.Find ("player").GetComponent<Rigidbody2D> ();
		transform = this.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		// Camera leads player movement
		Vector3 target = new Vector3 (rb2d.velocity.x * xfactor, rb2d.velocity.y * yfactor, -15f);
		this.transform.localPosition = Vector3.SmoothDamp (this.transform.localPosition, target, ref velocity, smooth);
	}
}
