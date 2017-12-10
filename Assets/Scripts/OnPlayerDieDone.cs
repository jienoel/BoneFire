using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDieDone : MonoBehaviour
{

    public void OnDieDone()
    {
        Game.Instance.player.OnDieDone();
    }
}
