using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            transform.parent = other.transform;
            transform.localPosition = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
