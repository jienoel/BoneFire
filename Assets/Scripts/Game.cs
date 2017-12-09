using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GameSignals.onPlayerDie += OnPlayerDie;
        GameSignals.onFirePileExtinguish += OnFirePileExtinguish;
    }

    void OnDestroy()
    {
        GameSignals.onPlayerDie -= OnPlayerDie;
        GameSignals.onFirePileExtinguish -= OnFirePileExtinguish;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    void OnPlayerDie()
    {
        
    }

    void OnFirePileExtinguish()
    {
        
    }

}
   
