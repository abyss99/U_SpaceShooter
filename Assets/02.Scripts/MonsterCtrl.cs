using UnityEngine;
using System.Collections;

public class MonsterCtrl : MonoBehaviour {
	public enum State { idle, trace, attack }
	public State state = State.idle;
	public bool isDie = false;

	public Transform playerTr;
	public Transform monsterTr;
	private NavMeshAgent nvAgent;

	public float traceDist = 10.0f;
	public float attackDist = 2.0f;

	private Animator anim;

	// Use this for initialization
	void Start () {
		playerTr = GameObject.FindGameObjectWithTag ("PLAYER").GetComponent<Transform>();
		monsterTr = this.gameObject.GetComponent<Transform> ();
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		StartCoroutine (CheckMonsterState ());
		//== StartCoroutine("CheckMonsterState");
		StartCoroutine (MonsterAction());
	}

	void Update() {

	}

	IEnumerator CheckMonsterState() {
		while (!isDie) {
			yield return new WaitForSeconds (0.3f);

			float dist = Vector3.Distance (playerTr.position, monsterTr.position);
			if (dist <= attackDist) {
				state = State.attack;
			} else if (dist <= traceDist) {
				state = State.trace;
			} else {
				state = State.idle;
			}
		}
	}

	IEnumerator MonsterAction() {
		while (!isDie) {
			yield return new WaitForSeconds (0.3f);

			switch (state) 
			{
				case State.idle:
					nvAgent.Stop ();
					anim.SetBool ("IsTrace", false);
					break;
				case State.trace:
					nvAgent.SetDestination (playerTr.position);
					nvAgent.Resume ();
					anim.SetBool ("IsTrace", true);
					break;
				case State.attack:
					break;
				
			}
		}
	}

}
