using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public Sprite lightedAltar;
    public Sprite unLightedAltar;
    public SpriteRenderer altarRender;

    public SpriteRenderer redDiamondRender;
    public SpriteRenderer yellowDiamondRender3;
    public SpriteRenderer blueDiamondRender2;

    bool _isLightAltar;

    void Start()
    {
        GameSignals.onPutDiamond += OnPutDiamond;
        redDiamondRender.enabled = false;
        yellowDiamondRender3.enabled = false;
        blueDiamondRender2.enabled = false;
    }

    void OnDestroy()
    {
        GameSignals.onPutDiamond -= OnPutDiamond;
    }

    public bool isLightAltar
    {
        get
        {
            return _isLightAltar;
        }
        set
        {
            if (_isLightAltar != value)
            {
                if (value)
                {
                    altarRender.sprite = lightedAltar;
                }
                else
                {
                    altarRender.sprite = unLightedAltar;
                }
            }
            _isLightAltar = value;
        }
    }

    public void OnPutDiamond(int color)
    {
        if (color == ColorTable.Blue && !blueDiamondRender2.enabled)
        {
            blueDiamondRender2.enabled = true;
            diamondFlag = diamondFlag | 1 << ColorTable.Blue;
        }
        else if (color == ColorTable.Yellow && !yellowDiamondRender3.enabled)
        {
            yellowDiamondRender3.enabled = true;
            diamondFlag = diamondFlag | 1 << ColorTable.Yellow;
        }
        else if (color == ColorTable.Red && !redDiamondRender.enabled)
        {
            redDiamondRender.enabled = true;
            diamondFlag = diamondFlag | 1 << ColorTable.Red;
        }

    }

    int _diamondFlag;

    public int diamondFlag
    {
        get
        {
            return _diamondFlag;
        }
        set
        {
            if (_diamondFlag != value)
            {
                if (value == 7)
                {
                    Debug.Log("宝石收集完毕");
                    GameSignals.InvokeAction(GameSignals.onFinishDiamond);
                }
            }
            _diamondFlag = value;
        }
    }

}
