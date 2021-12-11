using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public GameObject gameStartbutton;

    void GameStartClicked()
    {
        Merry_Go_Round.Instance.gameStart = true;
        Merry_Go_Round.Instance.ResetGameSetting();
        gameStartbutton.SetActive(false);
    }
}
