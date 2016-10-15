using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerAnim
{
	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip runBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;
}

public class PlayerCtrl : MonoBehaviour
{

	public Transform mTransform;
	public Animation anim;
	public float moveSpeed = 5.0f;
	private float vertical;
	private float horizontal;
	private float rot;
	public PlayerAnim playerAnim;

	void Start ()
	{
		mTransform = GetComponent<Transform> ();
		anim = GetComponent<Animation> ();
		anim.clip = playerAnim.idle;
		anim.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		mTransform.position += Vector3.forward * 0.1f;
		/*
		 	Vector3.forward = Vector3(0, 0, 1)
		 	Vector3.right = Vector3(1, 0, 0)
		 	Vector3.up = Vector3(0, 1, 0)

			Vector3.one = Vector3(1, 1, 1)
			Vector3.zero = Vector3(0, 0, 0)
	 */

		vertical = Input.GetAxis ("Vertical"); //가속도 포함
		horizontal = Input.GetAxis ("Horizontal");
		rot = Input.GetAxis ("Mouse X");

//		mTransform.Translate (Vector3.forward * 0.1f * vertical);
//		mTransform.Translate (Vector3.right * 0.1f * horizontal);
		Vector3 dir = (Vector3.forward * vertical) + (Vector3.right * horizontal);
		mTransform.Translate (dir.normalized * Time.deltaTime * moveSpeed); //normalized는 방향 벡터만 추출한 값이다, deltaTime은 지난 프레임과 현재 프레임 소요 시간
		mTransform.Rotate (Vector3.up * 80.0f * Time.deltaTime * rot);

		if (vertical >= 0.1f) {
			anim.CrossFade (playerAnim.runForward.name, 0.3f);
		} else if (vertical <= -0.1f) {
			anim.CrossFade (playerAnim.runBackward.name, 0.3f);
		} else if (horizontal >= 0.1f) {
			anim.CrossFade (playerAnim.runRight.name, 0.3f);
		} else if (horizontal <= -0.1f) {
			anim.CrossFade (playerAnim.runLeft.name, 0.3f);
		} else {
			anim.CrossFade (playerAnim.idle.name, 0.3f);
		}
	}

}