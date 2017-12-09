using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTreeOnFire : MonoBehaviour {
    public float maxDistance;

    private Monster monster;

    private void Start() {
        monster = GetComponent<Monster>();
    }

    private void Update() {
        var trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (var t in trees) {
            if (Vector3.Distance(t.transform.position, transform.position) < maxDistance && t.GetComponent<HitAndFire>().isOnFire) {
                monster.foodTarget = t.transform;
                break;
            }
        }
    }
}
