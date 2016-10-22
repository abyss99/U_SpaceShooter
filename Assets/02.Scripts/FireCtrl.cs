using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

	public Transform firePos;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Instantiate (bullet, firePos.position, firePos.rotation);
		}
	}
}
