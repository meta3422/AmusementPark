using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : BaseUI
{
    [Header("º≥∏Ì√¢")]
    public GameObject Panel_Description;
    
    public void OnClickGameStart()
    {
        StartUI_Manager.Inst.ChangeUI(UI_Type.GamePlay);
    }

    public void OnClickGameDesc()
    {
        Panel_Description.SetActive(true);
    }

    public void OnClickDescOK()
    {
        Panel_Description.SetActive(false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
