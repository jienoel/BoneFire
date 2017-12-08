using System;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [Header("Input")]
    public float thresholdTime = 0.1f; // 防误触，每次攻击要按下一定时间
    public LayerMask groundLayer;

    [Header("Attack")]
    public float maxDistance = 10f; // 最大手指/鼠标拖动范围
    public float maxDegree = 45;
    public float force = 10f;
    public GameObject bulletPrefab;

    private bool isClicked = false;
    private float clickTime = 0f;
    private Vector3 clickPos = Vector3.zero;

    private Vector3 GetGroundPoint() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, groundLayer)) {
            return hit.point;
        }
        return Vector3.down;
    }

    private void OnMouseDown() {
        isClicked = true;
        clickTime = Time.time;
        clickPos = GetGroundPoint();
    }

    private void Update() {
        if (isClicked) {
            if (Input.GetMouseButtonUp(0)) {
                var releaseTime = Time.time;
                if (releaseTime - clickTime < thresholdTime) {
                    isClicked = false;
                }
                else {
                    var releasePos = GetGroundPoint();
                    Debug.Log(string.Format("Click pos: {0}, release pos: {1}", clickPos, releasePos));
                    if (clickPos != Vector3.down && releasePos != Vector3.down) {
                        HandleThrow(releasePos - clickPos);
                    }
                }
            }
        }
    }

    private void HandleThrow(Vector3 dir) {
        var direction = -dir.normalized;
        var magnitude = Mathf.Clamp(dir.magnitude, 0, maxDistance);
        var anotherVec = Mathf.Lerp(0, Mathf.Tan(Mathf.Deg2Rad * maxDegree), magnitude / maxDistance) * Vector3.up;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Debug.Log(string.Format("direction: {0}, forceVec: {1}, final: {2}", direction, anotherVec, (direction + anotherVec.normalized)));
        bullet.GetComponent<Rigidbody>().AddForce((direction + anotherVec).normalized * force, ForceMode.Impulse);
    }
}
