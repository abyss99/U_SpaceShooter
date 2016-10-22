using UnityEngine;
using System.Collections;

public class BarrelCtrl : MonoBehaviour {
	public Texture[] textures;
	public GameObject explosionEffect;
	private int hitCount;
	private MeshRenderer _renderer;

	void Start() {
		_renderer = this.gameObject.GetComponentInChildren<MeshRenderer> ();

		int idx = Random.Range (0, textures.Length);
		_renderer.material.mainTexture = textures [idx];
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.collider.CompareTag("BULLET")) {
			hitCount++;
			if (hitCount >= 3) {
				hitCount = -100;
				ExpBarrel ();
			}
		}
	}

	void ExpBarrel() {
		Object obj = Instantiate (explosionEffect, transform.position, Quaternion.identity);
		Destroy (obj, 2.0f);
		Rigidbody rbody = this.gameObject.AddComponent<Rigidbody> ();
		rbody.AddForce (Vector3.up * 1800.0f);
		Destroy (this.gameObject, 3.0f);
	}

}
