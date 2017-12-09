using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndChangeColor : MonoBehaviour {
    public int colorIndex;
    private Color[] color = { Color.red, Color.yellow, Color.cyan };
    private Renderer renderer;

    public MonsterBody body;

    private void Start() {
        body.EnableBody(colorIndex);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            colorIndex = (colorIndex + 1) % ColorTable.Max;
            body.ChangeColor(colorIndex);
            GetComponentInParent<Monster>().HitByPlayer();
        }
    }
}
