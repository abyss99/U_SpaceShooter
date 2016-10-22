using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

	public Transform firePos;
	public GameObject bullet;
	public AudioClip fireSfx;
	public MeshRenderer muzzleFlash;

	private AudioSource _audio;


	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();
		muzzleFlash.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}
	}

	void Fire() {
		Instantiate (bullet, firePos.position, firePos.rotation);
		_audio.PlayOneShot(fireSfx, 1.0f);
		//_audio.clip = fireSfx;
		//_audio.Play ();
		StartCoroutine(ShowMuzzleFlash());

	}

	IEnumerator ShowMuzzleFlash() {
		float angle = Random.Range (0.0f, 360.0f);
		muzzleFlash.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
		float scale = Random.Range (1.5f, 3.0f);

		muzzleFlash.transform.localScale = Vector3.one * scale; //균등 스케일
		muzzleFlash.enabled = true;
		yield return new WaitForSeconds (Random.Range(0.05f, 0.2f));
		muzzleFlash.enabled = false;
	}
}
