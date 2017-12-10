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
           
            Game.FlipSprite(render, -transform.position + lastPosition);
            lastPosition = transform.position;
        }
    }

    void OnJumpGround()
    {
        if (bodyID == EBodyID.Zero)
        {
            monster.JumpDone(this);
        }
    }

    public void EnableBody(int colorID)
    {
        if (!init)
            Init();
        render.enabled = true;
        animator.enabled = true;
        ChangeColor(colorID, false);
    }

    public void ChangeColor(int colorID, bool needFlash = true)
    {
        if (ColorTable.isColorValid(colorID))
        {
            if (needFlash) {
                StartCoroutine(HitAndFlash());
            }
            animator.SetInteger(AnimatorParam.Color, colorID);
            Debug.Log("set color:" + colorID);
            colorIndex = colorID;
        }
    }

    bool changeColor1;
    bool changeColor2;
    int color1;
    int color2;

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

    private IEnumerator HitAndFlash() {
        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.3f);
            render.material.SetFloat("_FlashAmount", 0.75f);
            yield return new WaitForSeconds(0.2f);
            render.material.SetFloat("_FlashAmount", 0);
        }
    }

}
