using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour {
    private Color c;
    private SpriteRenderer sr;

    private void Start() {
        c = new Color(1, 1, 1, 0.75f);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        Color cc = new Color(1, 1, 1, 1 - Mathf.PingPong(0.4f * Time.time, 0.25f));
        transform.localScale = (1 - Mathf.PingPong(0.02f * Time.time, 0.03f)) * Vector3.one;
        sr.color = cc;
	}
}
