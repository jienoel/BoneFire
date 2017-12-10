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
    Vector3 lastPosition;
    public float thresholdDistance = 0.01f;

    void Awake()
    {
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSafe)
            return;
        if (other.tag == "Player")
        {
            target = Game.Instance.player.gameObject;
        }
    }

    void LateUpdate()
    {
        if (lastPosition != transform.position)
        {
            if (agent.speed > 0)
                Game.FlipSprite(render, transform.position - lastPosition);
            lastPosition = transform.position;
        }
    }

    void Update()
    {
        if (isSafe)
        {
            animator.SetFloat(AnimatorParam.FloatSpeed, 0);
            return;
        }
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        if (Vector3.Distance(lastPosition, transform.position) >= thresholdDistance)
            animator.SetFloat(AnimatorParam.FloatSpeed, agent.velocity.magnitude);
        else
            animator.SetFloat(AnimatorParam.FloatSpeed, 0);
    }


}
