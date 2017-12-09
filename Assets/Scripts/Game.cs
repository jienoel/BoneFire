using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public static  void FlipSprite(SpriteRenderer render, Vector3 forward)
    {
        Vector3 v = Vector3.Project(forward, Vector3.right);
        if (render.flipX != (v.x < 0))
            render.flipX = v.x < 0;
    }

    public static void SetAnimatorParam(AnimatorParamType paramType, Animator animator, string name, object value)
    {
        switch (paramType)
        {
            case AnimatorParamType.Float:
                SetAnimatorFloat(animator, name, (float)value);
                break;
            case AnimatorParamType.Bool:
                SetAnimatorBool(animator, name, (bool)value);
                break;
            case AnimatorParamType.Int:
                SetAnimatorInt(animator, name, (int)value);
                break;
            case AnimatorParamType.Trigger:
                SetAnimatorTrigger(animator, name);
                break;
        }
    }

    
    public static void SetAnimatorFloat(Animator animator, string name, float value)
    {
        animator.SetFloat(name, value);
    }

    public static void SetAnimatorTrigger(Animator animator, string name)
    {
        animator.SetTrigger(name);
    }

    public static void SetAnimatorBool(Animator animator, string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public static void SetAnimatorInt(Animator animator, string name, int value)
    {
        animator.SetInteger(name, value);
    }

    public static Game Instance;

    public Shooter player;
    public List<Transform> firedTrees = new List<Transform>();
    // Use this for initialization

    void Awake()
    {
        Instance = this;
        player = GetComponent<Shooter>();
        if (player == null)
        {
            Debug.LogError("player is null!");
        }
//        Debug.LogError(this.name);
    }

    void Start()
    {
        GameSignals.onPlayerDie += OnPlayerDie;
        GameSignals.onFirePileExtinguish += OnFirePileExtinguish;
        GameSignals.onTreeFired += OnTreeFired;
        GameSignals.onTreeDestroy += OnTreeDestroyed;
    }

    void OnDestroy()
    {
        GameSignals.onPlayerDie -= OnPlayerDie;
        GameSignals.onFirePileExtinguish -= OnFirePileExtinguish;
        GameSignals.onTreeFired -= OnTreeFired;
        GameSignals.onTreeDestroy -= OnTreeDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    void OnTreeFired(Transform tree)
    {
        if (!firedTrees.Contains(tree))
        {
            firedTrees.Add(tree);
        }
    }

    void OnTreeDestroyed(Transform tree)
    {
        if (firedTrees.Contains(tree))
        {
            firedTrees.Remove(tree);
        }
    }

    public bool GetNearestFiredTreeInRange(Vector3 position, float range, out  Transform tree)
    {
        float distance = range;
        tree = null;
        for (int i = 0; i < firedTrees.Count; i++)
        {
            float dis = Vector3.Distance(position, firedTrees[i].position);
            if (dis <= distance)
            {
                distance = dis;
                tree = firedTrees[i];
            }
        }
        return tree != null;
    }

    void OnPlayerDie()
    {
        
    }

    void OnFirePileExtinguish()
    {
        
    }

    void GameOver()
    {
        
    }

}


   
