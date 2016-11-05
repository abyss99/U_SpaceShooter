using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

	public Transform firePos;
	public GameObject bullet;
	public AudioClip fireSfx;
	public MeshRenderer muzzleFlash;

	private AudioSource _audio;
	public Light fireFlash;

	public float fireRate = 0.1f;
	private float nextFire = 0.0f;
	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();
		muzzleFlash.enabled = false;
		fireFlash.intensity = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

		if (Input.GetMouseButton (0)) {
			if (Time.time >= nextFire)
			{
				Fire();

				if(Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f, 1<<9)) { //9번 레어만 검출한다는 의미
						hit.collider.GetComponent<MonsterCtrl>().OnDamage(hit.point, 20.0f); 
				}

				if(Physics.Raycast(firePos.position, firePos.forward, out hit, 20.0f, 1<<11)) { //9번 레어만 검출한다는 의미
					hit.collider.GetComponent<BarrelCtrl>().OnDamage(); 
				}

				nextFire = Time.time + fireRate;
			}
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
		fireFlash.intensity = Random.Range(4.0f, 5.5f);
		yield return new WaitForSeconds (Random.Range(0.05f, 0.2f));
		muzzleFlash.enabled = false;
		fireFlash.intensity = 0.0f;

	}
}
