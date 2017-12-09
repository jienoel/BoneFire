using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndFire : MonoBehaviour {
    public float burnTime;
    public float rescueTime;

    public bool isOnFire;

    private Coroutine burn_CO;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log(string.Format("{0} is on fire", name));
            if (burn_CO != null) {
                isOnFire = true;
                burn_CO = StartCoroutine(Burning());
            }
        }
        if (collision.gameObject.tag == "Monster") {
            if (burn_CO != null) {
                StopCoroutine(burn_CO);
            }
            burn_CO = StartCoroutine(Rescue(collision.gameObject.GetComponentInParent<MonsterStack>()));
        }
    }

    private IEnumerator Burning() {
        yield return new WaitForSeconds(burnTime);
        Destroy(transform.parent.gameObject);
    }

    private IEnumerator Rescue(MonsterStack monster) {
        yield return new WaitForSeconds(rescueTime);
        Debug.Log("Rescued");
        isOnFire = false;
    }
}
