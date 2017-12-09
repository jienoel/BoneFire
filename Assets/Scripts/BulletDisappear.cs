using UnityEngine;

public class BulletDisappear : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
