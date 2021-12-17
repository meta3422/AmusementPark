using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultUI : BaseUI
{
    [Header("점수 텍스트")]
    public TextMeshProUGUI Txt_Score;

    [Header("이 UI에서 관찰할 바이킹 오브젝트")]
    public GameObject Viking;

    // Start is called before the first frame update
    void OnEnable()
    {
        Txt_Score.text = GameStateManager.Inst.score.ToString() + "점";   
    }

    public void OnClickToTitle()
    {
        StartUI_Manager.Inst.ChangeUI(UI_Type.Title);
    }

    public override void Deactivate()
    {
        Viking.SetActive(false);
        base.Deactivate();
    }
}
