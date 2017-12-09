using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndFire : MonoBehaviour {
    public float burnTime;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log(string.Format("{0} is on fire", name));
        }
        Destroy(transform.parent.gameObject, burnTime);
    }
}
