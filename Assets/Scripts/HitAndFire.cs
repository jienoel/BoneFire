using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndFire : MonoBehaviour, IChaseable
{
    public float burnTime;
    public float rescueTime;
    public Transform tree;
    public SpriteRenderer treeRender;
    public SpriteRenderer ashRender;
    public Animator fireAnimator;
    public SpriteRenderer fireRender;
    public bool isOnFire;

    private Coroutine burn_CO;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log(string.Format("{0} is on fire", name));
            if (burn_CO == null)
            {
                isOnFire = true;
                GameSignals.InvokeAction(GameSignals.onTreeFired, tree);
                StartCoroutine(HitAndFlash());
                burn_CO = StartCoroutine(Burning());
            }
        }
    }

    private IEnumerator Burning()
    {
        fireAnimator.enabled = true;
        fireRender.enabled = true;
        yield return new WaitForSeconds(burnTime);
        OnTreeDestroy();
    }

    private IEnumerator Rescue()
    {
        yield return new WaitForSeconds(rescueTime);
        Debug.Log("Rescued");
        isOnFire = false;
        OnTreeDestroy();
    }

    void OnTreeDestroy()
    {
        GameSignals.InvokeAction(GameSignals.onTreeDestroy, tree);
        treeRender.enabled = false;
        fireRender.enabled = false;
        ashRender.enabled = true;
        //Destroy(transform.parent.gameObject);
    }

    public void Arrived()
    {
        Debug.Log("burnArrive");
        if (burn_CO != null)
        {
            StopCoroutine(burn_CO);
        }
        burn_CO = StartCoroutine(Rescue());
    }

    private IEnumerator HitAndFlash() {
        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.3f);
            treeRender.material.SetFloat("_FlashAmount", 0.75f);
            yield return new WaitForSeconds(0.2f);
            treeRender.material.SetFloat("_FlashAmount", 0);
        }
    }

}
