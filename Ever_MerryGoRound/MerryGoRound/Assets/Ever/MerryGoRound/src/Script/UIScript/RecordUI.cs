using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{
    public Text bestRecord;
    public Text currentRecord;
    public Text lapse;
    void Start()
    {
        bestRecord.text = PlayerPrefs.GetInt("BestRecord", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRecord();
    }

    void ChangeRecord()
    {
        if (Merry_Go_Round.Instance.gameStart)
        {
            lapse.text = Merry_Go_Round.Instance.lapse.ToString();
            currentRecord.text = ((int)Merry_Go_Round.Instance.Record).ToString();
        }
        else
        {
            if (((int) Merry_Go_Round.Instance.Record) > PlayerPrefs.GetInt("BestRecord", 0))
            {
                PlayerPrefs.SetInt("BestRecord", ((int)Merry_Go_Round.Instance.Record));
                bestRecord.text = ((int)Merry_Go_Round.Instance.Record).ToString();
            }
        }


    }
}
