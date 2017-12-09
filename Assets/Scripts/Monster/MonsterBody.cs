using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    public int BodyID;
    public Animator animator;
    SpriteRenderer render;

    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (render == null)
        {
            render = GetComponent<SpriteRenderer>();
        }
//        animator.enabled = false;
//        render.sprite = null;
//        render.enabled = false;
    }

    public void EnableBody(int colorID)
    {
        render.enabled = true;
        animator.enabled = true;
        ChangeColor(colorID);
    }

    public void ChangeColor(int colorID)
    {
        if (ColorTable.isColorValid(colorID))
            animator.SetFloat(AnimatorParam.Color, colorID);
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
