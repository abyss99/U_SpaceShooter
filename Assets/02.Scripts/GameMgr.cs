using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {
    public static GameMgr instance;

    public GameObject monster;
    public Transform[] points;

    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

    public float createTime = 5.0f;
    public bool isGameOver = false;

    public Text scoreLabel;
    public int highScore = 0;

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }
	// Use this for initialization
	void Start () {
        highScore = PlayerPrefs.GetInt("SCORE", 0);
        DisplayScore(0);
        monster = Resources.Load<GameObject>("Prefabs/monster");
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        for (int i = 0; i < maxPool; i++)
        {
            GameObject _monster = (GameObject)Instantiate(monster);
            _monster.name = "Monster_" + i.ToString("00");
            _monster.SetActive(false);
            monsterPool.Add(_monster);
        }
        StartCoroutine(this.CreateMonster());
	}

    IEnumerator CreateMonster() {
        yield return new WaitForSeconds(2.0f);
        while (!isGameOver)
        {
            foreach (var _monster in monsterPool)
            {
                if (!_monster.activeSelf)
                {
                    int idx = Random.Range(1, points.Length);
                    _monster.transform.position = points[idx].position;
                    _monster.transform.LookAt(points[0].position);
                    _monster.SetActive(true);
                    break;
                }
            }

            yield return new WaitForSeconds(createTime);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayScore(int score) {
        highScore += score;

        PlayerPrefs.SetInt("SCORE", highScore);
        scoreLabel.text = "SCORE : <color=#ff0000>" + highScore + "</color>";
    }
}
