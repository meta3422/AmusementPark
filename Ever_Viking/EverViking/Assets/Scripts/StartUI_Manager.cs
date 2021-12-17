using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_Type
{
    Title,
    GamePlay,
    Finish
}

public class StartUI_Manager : MonoBehaviour
{
    public static StartUI_Manager uiManager;

    public static StartUI_Manager Inst
    {
        get
        {
            return uiManager;
        }
    }

    private BaseUI curUI;

    [Header("게임 UI 목록")]
    public TitleUI UI_Title;
    public GamePlayUI UI_GamePlay;
    public ResultUI UI_Result;

    // Start is called before the first frame update
    void Start()
    {
        InitManager();
        SelectFirstUI();
    }

    // 첫 UI를 정해준다.
    private void SelectFirstUI()
    {
        UI_Title.Deactivate();
        UI_GamePlay.Deactivate();
        UI_Result.Deactivate();

        curUI = UI_Title;

        curUI.Activate();
    }

    // 싱글턴 초기화를 진행한다.
    private void InitManager()
    {
        if(uiManager)
        {
            Destroy(this);
            return;
        }
        uiManager = this;
    }

    public void ChangeUI(UI_Type type)
    {
        curUI.Deactivate();

        switch(type)
        {
            case UI_Type.Title:
                curUI = UI_Title;
                break;
            case UI_Type.GamePlay:
                curUI = UI_GamePlay;
                break;
            case UI_Type.Finish:
                curUI = UI_Result;
                break;
        }

        curUI.Activate();
    }
}
