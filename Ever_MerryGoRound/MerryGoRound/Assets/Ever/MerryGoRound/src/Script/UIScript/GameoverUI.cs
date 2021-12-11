using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    static public GameObject gameovertext;
    public GameObject gameStartbutton;
    void Start()
    {
        gameovertext = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Merry_Go_Round.Instance.gameStart)
        { 
            gameStartbutton.SetActive(true);
        }
        else
        {
            gameovertext.SetActive(false);
        }
    }

    public static void ActiveGameOverTextTrue()
    {
        gameovertext.SetActive(true);
    }
}
