using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static public GameStateManager gameStateManager;
    static public GameStateManager Inst { get{ return gameStateManager; } }

    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        InitManager();
    }

    private void InitManager()
    {
        gameStateManager = this;
    }
}
