using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameSignals
{
    public static Action onPlayerDie;
    public static Action onFirePileExtinguish;
    public static Action onPlayerRelive;
    public static Action<Transform> onTreeFired;
    public static Action<Transform> onTreeDestroy;

    public static Action<int> onPutDiamond;
    public static Action<int> onPlayerHitDiamond;
    public static Action<string> onTrigeSafeArea;

    public static void InvokeAction(Action action)
    {
        if (action != null)
        {
            action();
        }
    }

    public static void InvokeAction<T>(Action<T> action, T param)
    {
        if (action != null)
        {
            action(param);
        }
    }


}


