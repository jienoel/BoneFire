using UnityEngine;

public class BulletDisappear : MonoBehaviour {
    private void Update() {
        transform.right = GetComponent<Rigidbody>().velocity;
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
