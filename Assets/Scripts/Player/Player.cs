using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public int hp;
    public int hpMax = 100;
    public Animator animator;
    // Use this for initialization
    public bool isDie
    {
        get
        {
            return hp <= 0;
        }
    }

    void Start()
    {
        hp = hpMax;
        animator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }


    void OnPlayerWasAttack(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetBool(AnimatorParam.BoolDie, true);
            GameSignals.InvokeAction(GameSignals.onPlayerDie);
            return;
        }
    }

    public void RelivePlayer()
    {
        hp = hpMax;
        animator.SetBool(AnimatorParam.BoolRelive, true);
    }
}
