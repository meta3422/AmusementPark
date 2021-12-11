using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    static public GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newGameObject = new GameObject();
                _instance = newGameObject.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public int Combo { get; set; }

    
}
