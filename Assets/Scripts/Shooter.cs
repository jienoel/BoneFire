using System;
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

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
        if (Input.GetKeyDown(KeyCode.A)) {
            isClicked = false;
        }
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
        else {
            if (Input.GetMouseButtonDown(0)) {
                var dest = GetGroundPoint();
                if (dest != Vector3.down) {
                    agent.SetDestination(dest);
                }
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
        if (canDisplay) {
            var v0 = dir.y * force;
            var x0 = transform.position.y;
            var g = Physics.gravity.y;
            var t = (-v0 - Mathf.Sqrt(v0 * v0 - 2 * g * x0)) / g;
            int count = (int)(t / Time.fixedDeltaTime);
            var positions = new Vector3[count];
            Vector3 startPos = transform.position;
            Vector3 vel = new Vector3(dir.x * force, v0, dir.z * force);
            for (int i = 0; i < count; i++) {
                startPos += vel * Time.fixedDeltaTime;
                vel += Physics.gravity * Time.fixedDeltaTime;
                positions[i] = startPos;
            }
            //line.material.mainTextureScale = new Vector2(count / 4f, 1);
            line.positionCount = count;
            line.SetPositions(positions);
            line.startWidth = width;
            line.endWidth = width;
        }
        else {
            line.positionCount = 0;
        }
    }
}
