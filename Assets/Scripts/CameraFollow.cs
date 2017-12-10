using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    private Vector3 offset;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    public float xWindow;
    public float yWindow;

	// Use this for initialization
	void Start () {
        offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var position = target.position - offset;
        if (Mathf.Abs(transform.position.x - position.x) > xWindow) {
            if (transform.position.x < position.x) position.x -= xWindow;
            if (transform.position.x > position.x) position.x += xWindow;
        }
         if (Mathf.Abs(transform.position.z - position.z) < yWindow) {
            if (transform.position.z < position.z) position.z -= yWindow;
            if (transform.position.z > position.z) position.z += yWindow;
        }
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.z = Mathf.Clamp(position.z, yMin, yMax);
        position.y = 6.9f;
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
	}
}
