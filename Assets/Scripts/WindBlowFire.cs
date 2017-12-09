using System;
using UnityEngine;
/// <summary>
/// 控制吹风，当玩家位于Trigger内时表示安全，外部可以调用UpdateWind接口来随机新的风的大小
/// </summary>
public class WindBlowFire : MonoBehaviour {
    // 用于调试
    public bool needUpdate;
    public Light fireColorArea;

    public float windMagnitude;
    public float degree;
    public float speed;

    private float lightHeight;
    private System.Random rnd;

    private void Start() {
        lightHeight = fireColorArea.transform.position.y;
        rnd = new System.Random();
    }

    private void Update() {
        var target = Quaternion.Euler(0, degree, 0) * Vector3.right * windMagnitude;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        fireColorArea.transform.position = Vector3.Slerp(transform.position, target, speed * Time.deltaTime) + lightHeight * Vector3.up;
        if (needUpdate) {
            UpdateWind();
            needUpdate = false;
        }
    }

    public void UpdateWind() {
        windMagnitude = (float)rnd.NextDouble() * 3f;
        degree = (float)rnd.NextDouble() * 360f;
    }
}
