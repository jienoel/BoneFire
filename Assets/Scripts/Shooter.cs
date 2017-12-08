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

    [Header("Hint")]
    public LineRenderer line;
    public float width;
    public float length;

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
            var currPoint = GetGroundPoint();
            if (clickPos != Vector3.down && currPoint != Vector3.down) {
                transform.forward = clickPos - currPoint;
            }
            var currDir = CalculateDirection(currPoint - clickPos);
            DisplayLine(true, currDir);
            if (Input.GetMouseButtonUp(0)) {
                var releaseTime = Time.time;
                if (releaseTime - clickTime < thresholdTime) {
                }
                else {
                    if (clickPos != Vector3.down && currPoint != Vector3.down) {
                        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody>().AddForce(currDir * force, ForceMode.Impulse);
                        DisplayLine(false, currDir);
                    }
                }
                isClicked = false;
            }
        }
    }

    private Vector3 CalculateDirection(Vector3 dir) {
        var direction = -dir.normalized;
        var magnitude = Mathf.Clamp(dir.magnitude, 0, maxDistance);
        var anotherVec = Mathf.Lerp(0, Mathf.Tan(Mathf.Deg2Rad * maxDegree), magnitude / maxDistance) * Vector3.up;
        return (direction + anotherVec).normalized;
    }

    private void DisplayLine(bool canDisplay, Vector3 dir) {
        line.SetPositions(new Vector3[] { transform.position, transform.position + dir * length });
        line.startWidth = width;
        line.endWidth = width;
    }
}
