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
        }
        else if (color == ColorTable.Yellow && !yellowDiamondRender3.enabled)
        {
            yellowDiamondRender3.enabled = true;
        }
        else if (color == ColorTable.Red && !redDiamondRender.enabled)
        {
            redDiamondRender.enabled = true;

        }
    }


}
