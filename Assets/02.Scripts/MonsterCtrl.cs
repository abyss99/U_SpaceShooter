using UnityEngine;
using System.Collections;

public class MonsterCtrl : MonoBehaviour
{
	public enum State
	{
		idle,
		trace,
		attack

	}

	public State state = State.idle;
	public bool isDie = false;

	public Transform playerTr;
	public Transform monsterTr;
	private NavMeshAgent nvAgent;

	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	private float hp = 100.0f;
	public GameObject bloodEffect;
	public GameObject bloodDecal;

	private Animator anim;

	void OnEnable() {
		PlayerCtrl.OnPlayerDie += this.YouWin;
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
	}

	void OnDisable() {
		PlayerCtrl.OnPlayerDie -= this.YouWin;
	}
	// Use this for initialization
	void Awake()
	{
		playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
		monsterTr = this.gameObject.GetComponent<Transform>();
		nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();

		bloodEffect = Resources.Load<GameObject>("Prefabs/BloodEffect");
		bloodDecal = Resources.Load<GameObject>("Prefabs/BloodDecal");
	}

	IEnumerator CheckMonsterState()
	{
		while (!isDie)
		{
			yield return new WaitForSeconds(0.3f);

			float dist = Vector3.Distance(playerTr.position, monsterTr.position);
			if (dist <= attackDist)
			{
				state = State.attack;
			}
			else if (dist <= traceDist)
			{
				state = State.trace;
			}
			else
			{
				state = State.idle;
			}
		}
	}

	IEnumerator MonsterAction()
	{
		while (!isDie)
		{
			yield return new WaitForSeconds(0.3f);

			switch (state)
			{
				case State.idle:
					nvAgent.Stop();
					anim.SetBool("IsTrace", false);
					break;
				case State.trace:
					nvAgent.SetDestination(playerTr.position);
					nvAgent.Resume();
					anim.SetBool("IsAttack", false);
					anim.SetBool("IsTrace", true);
					break;
				case State.attack:
					anim.SetBool("IsAttack", true);
					break;
				
			}
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "BULLET")
		{
			anim.SetTrigger("Hit");

			ShowBloodEffect(coll.transform.position);
			hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
			if (hp <= 0.0f)
			{
				MonsterDie();
			}
			Destroy(coll.gameObject);
		}
	}

	void ShowBloodEffect(Vector3 pos) {
		Object obj = Instantiate(bloodEffect, pos, Quaternion.identity);
		Destroy(obj, 2.0f);
		GameObject decal = (GameObject)Instantiate(bloodDecal, monsterTr.position, Quaternion.identity);
		decal.transform.rotation = Quaternion.Euler(90, 0, Random.Range(0, 360));
		decal.transform.position += Vector3.up * 0.1f;
		decal.transform.localScale = Vector3.one * Random.Range(2.0f, 4.0f);
		Destroy(decal, 5.0f);
	}

	void MonsterDie() {
		anim.SetTrigger("Die");
		StopAllCoroutines();
		nvAgent.Stop();
		GetComponent<CapsuleCollider>().enabled = false;
        Invoke("ReturnMonsterToPooling", 5.0f);
	}

    void ReturnMonsterToPooling() {
        hp = 100.0f;
        GetComponent<CapsuleCollider>().enabled = true;

        this.gameObject.SetActive(false);
    }

	void OnTriggerEnter(Collider coll) {
		Debug.Log(coll.tag);
	}

	public void OnDamage(Vector3 pos, float damage) {
		anim.SetTrigger("Hit");

		ShowBloodEffect(pos);
		hp -= damage;
		if (hp <= 0.0f)
		{
			MonsterDie();
		}
	}

	void YouWin() {
		anim.SetTrigger("PlayerDie");
		StopAllCoroutines();
		nvAgent.Stop();
	}
}
