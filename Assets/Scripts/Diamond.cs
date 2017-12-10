using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int colorID;
    public Sprite[] sprites;
    public SpriteRenderer render;

    public void SetColor(int color)
    {
        render.sprite = sprites[color];
        colorID = color;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            //            transform.parent = other.transform;
            //            transform.localPosition = Vector3.zero;
            //            GetComponent<Rigidbody>().useGravity = false;
            //            GetComponent<Rigidbody>().isKinematic = true;
            GameSignals.InvokeAction(GameSignals.onPlayerHitDiamond, colorID);
            Destroy(transform.parent.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
//            transform.parent = other.transform;
//            transform.localPosition = Vector3.zero;
//            GetComponent<Rigidbody>().useGravity = false;
//            GetComponent<Rigidbody>().isKinematic = true;
            GameSignals.InvokeAction(GameSignals.onPlayerHitDiamond, colorID);
            Destroy(transform.parent.gameObject);
           
        }
    }
}
