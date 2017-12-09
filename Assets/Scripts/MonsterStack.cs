using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStack : MonoBehaviour {
    public HitAndChangeColor[] monsterParts;

    private void Update() {
        bool isSame = true;
        var c = monsterParts[0].colorIndex;
        foreach (var m in monsterParts) {
            if (m.colorIndex != c) {
                isSame = false;
                break;
            }
        }
        if (isSame) {
            Debug.Log(string.Format("{0} died.", name));
            Destroy(gameObject);
        }
    }
}
