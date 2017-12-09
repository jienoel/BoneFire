using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{

    public Transform target;
    NavMeshAgent navMeshAgent;
    public List<MonsterBody> monsterBodys = new List<MonsterBody>();
    public GameObject diamond;

    public float catchDistance;

    public void ResetBody()
    {
        monsterBodys.Clear();
        monsterBodys.AddRange(GetComponentsInChildren<MonsterBody>());
    }
    // Use this for initialization
    void Start()
    {
        ResetBody();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (CheckColor()) {
            Debug.Log(string.Format("{0} died.", name));
            Instantiate(diamond, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        if (target != null)
        {
            navMeshAgent.SetDestination(target.transform.position);
            Debug.Log(Vector3.Distance(transform.position, target.transform.position));
            if (Vector3.Distance(transform.position, target.transform.position) < catchDistance) {
                Debug.Log("Arrive");
                target.GetComponent<IChaseable>().Arrived();
                target = null;
            }
        }
    }

    private bool CheckColor() {
        bool isSame = true;
        var c = monsterBodys[0].colorIndex;
        foreach (var m in monsterBodys) {
            if (m.colorIndex != c) {
                isSame = false;
                break;
            }
        }
        return isSame;
    }

    public void HitByPlayer() {
        if (target != null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
