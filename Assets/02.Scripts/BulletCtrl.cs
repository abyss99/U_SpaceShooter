using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {

	//private Rigidbody rbody;

	// Use this for initialization
	void Start () {
		//rbody = this.gameObject.GetComponent<Rigidbody> ();
		//rbody.AddForce (transform.forward * 500.0f); // 자신의 전진 방향
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000.0f);
	}

}
