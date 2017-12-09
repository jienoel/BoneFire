using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndChangeColor : MonoBehaviour {
    public int colorIndex;
    private Color[] color = { Color.red, Color.yellow, Color.cyan };
    private Renderer renderer;

    private void Start() {
        renderer = GetComponent<Renderer>();
        renderer.material.color = color[colorIndex];
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            colorIndex = (colorIndex + 1) % 3;
            renderer.material.color = color[colorIndex];
        }
    }
}
