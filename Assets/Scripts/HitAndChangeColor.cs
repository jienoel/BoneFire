using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndChangeColor : MonoBehaviour
{
    private Color[] color = { Color.red, Color.yellow, Color.cyan };
    private Renderer renderer;

    public MonsterBody body;
    public Monster monster;

    private void Start()
    {
//        body.EnableBody(colorIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
           
            GetComponentInParent<Monster>().HitByPlayer(body);
        }
    }
}
