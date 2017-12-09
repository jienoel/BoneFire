using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    public int bodyID;
    public Animator animator;
    public int colorIndex;
    // set by hitAndChangeColor, read by monster
    SpriteRenderer render;
    Vector3 lastPosition;
    Monster monster;

    void Awake()
    {
        EnableBody(colorIndex);
        monster = GetComponentInParent<Monster>();
    }

    bool init;

    void Init()
    {
        if (init)
            return;
        init = true;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (render == null)
        {
            render = GetComponent<SpriteRenderer>();
        }
        animator.enabled = false;
        render.sprite = null;
        render.enabled = false;
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        if (lastPosition != transform.position)
        {
           
            Game.FlipSprite(render, transform.position - lastPosition);
            lastPosition = transform.position;
        }
    }

    void OnJumpGround()
    {
        if (bodyID == EBodyID.Zero)
        {
            Debug.Log("OnJumpGround " + this.gameObject.name);
            monster.JumpDone(this);
        }
    }

    public void EnableBody(int colorID)
    {
        if (!init)
            Init();
        render.enabled = true;
        animator.enabled = true;
        ChangeColor(colorID);
    }

    public void ChangeColor(int colorID)
    {
        if (ColorTable.isColorValid(colorID))
        {
            animator.SetInteger(AnimatorParam.Color, colorID);
            Debug.Log("set color:" + colorID);
            colorIndex = colorID;
        }
    }

    public  bool changeColor1;
    public bool changeColor2;
    public int color1;
    public  int color2;

    void Update()
    {
        if (changeColor1)
        {
            color1 = (color1 + 1) % ColorTable.Max;
            ChangeColor(color1);
            changeColor1 = false;
        }

        if (changeColor2)
        {
            color2 = (color2 + 1) % ColorTable.Max;
            render.color = color2 == ColorTable.Red ? Color.red : Color.green;
            changeColor2 = false;
        }
    }

}
