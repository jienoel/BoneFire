using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterAnimator : MonoBehaviour
{

    public Monster monster;

    public bool hasAtkTarget;
    bool _hasAtkTarget;

    [Range(0, 1)]
    public float speed;
    float _speed;

    public bool syncToFirst;
    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_hasAtkTarget != hasAtkTarget)
        {
            monster.SetAnimator(AnimatorParamType.Bool, AnimatorParam.BoolHasAtkTarget, hasAtkTarget);
            _hasAtkTarget = hasAtkTarget;
            Debug.Log("Set has Atk Target  " + hasAtkTarget);
        }
        if (speed != _speed)
        {
            monster.SetAnimator(AnimatorParamType.Float, AnimatorParam.FloatSpeed, speed);
            _speed = speed;
            Debug.Log("Set Speed " + speed);
        }

        if (syncToFirst)
        {
            foreach (MonsterBody body in monster.monsterBodies)
            {
                if (body.bodyID == 0)
                    continue;
                
                jumpToTime(body.animator, currentAnimationName(body.animator), monster.monsterBodies[0].animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
        }
    }


    void jumpToTime(Animator anim, string name, float nTime)
    {
        Debug.Log(anim.name + "  " + name + "  " + nTime);
        anim.Play(name, 0, nTime);
    }

    string currentAnimationName(Animator anim)
    {
        var currAnimName = "";
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;

    }

}
