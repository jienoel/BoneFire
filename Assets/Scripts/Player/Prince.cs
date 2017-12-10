using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prince : MonoBehaviour
{
    public bool isSafe = false;
    public SpriteRenderer render;
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (isSafe) return;
        if (other.tag == "Player")
        {
            target = Game.Instance.player.gameObject;
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        animator.SetFloat(AnimatorParam.FloatSpeed, agent.speed);
    }


}
