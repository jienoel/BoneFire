﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireTrigger : MonoBehaviour
{
    public int diamondCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shooter shooter = other.gameObject.GetComponentInParent<Shooter>();
            shooter.inSafeArea = true;
            if (shooter.diamondColor >= 0)
            {
                GameSignals.onPutDiamond(shooter.diamondColor);
            }
        }
        if (other.gameObject.tag == "Prince")
        {
            other.GetComponent<Prince>().target = null;
            other.GetComponent<Prince>().isSafe = true;
            other.GetComponent<Prince>().agent.Stop();
            other.transform.position = transform.position;
            Debug.Log("Prince Go Home");
            GameSignals.InvokeAction(GameSignals.onPrinceInSafeArea);
        }

        Debug.Log("Enter campFire trigger " + other.gameObject.tag + "  " + other.gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shooter shooter = other.gameObject.GetComponentInParent<Shooter>();
            if (shooter.diamondColor >= 0)
            {
                GameSignals.onPutDiamond(shooter.diamondColor);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shooter shooter = other.gameObject.GetComponentInParent<Shooter>();
            shooter.inSafeArea = false;
        }
        Debug.Log("Exit campFire trigger " + other.gameObject.tag + "  " + other.gameObject.name);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shooter shooter = other.gameObject.GetComponentInParent<Shooter>();
            shooter.inSafeArea = true;
        }
        Debug.Log("Enter campFire trigger " + other.gameObject.tag + "  " + other.gameObject.name);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shooter shooter = other.gameObject.GetComponentInParent<Shooter>();
            shooter.inSafeArea = false;
        }
        Debug.Log("Exit campFire trigger " + other.gameObject.tag + "  " + other.gameObject.name);
    }
}
