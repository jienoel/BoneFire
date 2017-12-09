using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterStack : MonoBehaviour {
    public HitAndChangeColor[] monsterParts;
    public GameObject diamond;

    private NavMeshAgent agent;
    private bool isChasing;
    private bool isTreeing;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        bool isSame = true;
        var c = monsterParts[0].colorIndex;
        foreach (var m in monsterParts) {
            if (m.colorIndex != c) {
                isSame = false;
                break;
            }
        }
        if (isSame) {
        }
        if (isChasing) {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }

    public void HitAndChase() {
        isChasing = true;
    }
}
