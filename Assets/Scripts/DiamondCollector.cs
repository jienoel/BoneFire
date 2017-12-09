using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCollector : MonoBehaviour {
    public int diamondCount;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Diamond") {
            if (other.isTrigger) {
                diamondCount++;
                Destroy(other.gameObject);
            }
        }
    }
}
