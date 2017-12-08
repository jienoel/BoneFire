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

	// Use this for initialization
	void Start () {
        offset = target.position - transform.position;
        Debug.Log(offset);
	}
	
	// Update is called once per frame
	void Update () {
        var position = target.position - offset;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.z = Mathf.Clamp(position.z, yMin, yMax);
        transform.position = position;
	}
}
