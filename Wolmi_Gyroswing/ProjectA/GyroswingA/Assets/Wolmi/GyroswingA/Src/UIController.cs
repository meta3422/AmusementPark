using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    float secondsMax;
    int enemyMax;

    public void InitUI(int secondsMax, int enemyMax)
    {
        this.secondsMax = secondsMax;
        this.enemyMax = enemyMax;

        timeText.text = GetCurrentTimeString(0.0f);
        enemyCountText.text = new StringBuilder("남은 적 0 / " + enemyMax).ToString();
    }

    public void UpdateTime(float time)
    {
        timeText.text = GetCurrentTimeString(time);
    }

    public void UpdateEnemyCount(int enemy)
    {
        enemyCountText.text = new StringBuilder("남은 적 " + enemy + " / " + enemyMax).ToString();
    }

    string GetCurrentTimeString(float seconds)
    {
        int sec = Mathf.FloorToInt(seconds);
        int min = 0;

        if (sec >= 60)
        {
            min = sec / 60;
            sec = sec % 60;
        }

        string res = min.ToString("00");
        res += " : ";
        res += sec.ToString("00");

        return res;
    }

}
