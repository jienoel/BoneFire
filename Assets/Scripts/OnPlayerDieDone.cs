using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDieDone : MonoBehaviour
{
    public bool isPlayAttack;

    public void OnDieDone()
    {
        Debug.Log("OnDieDone");
        Game.Instance.player.OnDieDone();
    }

    //    public void OnAttackDone()
    //    {
    //        isPlayAttack = false;
    //        Debug.Log("OnAttackDone");
    //        Game.Instance.player.animator.SetBool(AnimatorParam.boolAttack, false);
    //    }
    //
    //    public void OnAttackStart()
    //    {
    //        isPlayAttack = true;
    //        Debug.Log("OnAttackStart");
    //    }
}
