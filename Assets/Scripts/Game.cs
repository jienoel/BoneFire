using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [Serializable]
    public class GameSettings
    {
        public float someMovementSpeedThatMakesSense = 0.1f;
    }

    public   static Game Instance;

    public GameSettings settings;
    // Use this for initialization
    void Start()
    {
        Instance = this;
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

    void GameOver()
    {
        
    }

}


   
