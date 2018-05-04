using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextScript : MonoBehaviour {

	public GameObject obj;
	public float fadeTime;
	private Color alphaColor;

	// Use this for initialization
	void Start () {
		alphaColor = obj.GetComponent<MeshRenderer> ().material.color;
		alphaColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			obj.GetComponent<MeshRenderer> ().material.color = Color.Lerp (obj.GetComponent<MeshRenderer> ().material.color, alphaColor, fadeTime * Time.deltaTime);
		}
	}
}
