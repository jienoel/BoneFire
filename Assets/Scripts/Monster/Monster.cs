using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{

    public Transform target;
    NavMeshAgent navMeshAgent;
    public List<HitAndChangeColor> monsterBodys = new List<HitAndChangeColor>();

    public void ResetBody()
    {
        monsterBodys.Clear();
        monsterBodys.AddRange(GetComponentsInChildren<HitAndChangeColor>());
    }
    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
