using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : BaseUI
{
    // >>>> 가져올 게임 오브젝트 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [Header("타이머 이미지")]
    [Header("=============================================")]
    public Image IMG_Timer;

    [Header("점수 텍스트")]
    public TextMeshProUGUI TXT_Score;

    [Header("터치 기회 이미지들")]
    public Image[] IMG_TouchChances;

    [Header("기회 있음 & 없음 이미지")]
    public Sprite SPR_ChanceAvailable;
    public Sprite SPR_ChanceUsed;

    [Header("터치 버튼 프리팹")]
    public GameObject Prefab_TouchBtn;

    [Header("이 UI에서 관찰할 바이킹 오브젝트")]
    public PendulumMovement Viking;




    // >>>> 외부 조정 가능한 변수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [Header("게임 제한 시간")]
    [Header("=============================================")]
    public float TIME_LIMIT = 60f;

    [Header("터치 버튼 인터벌")]
    public float INTERVAL_TOUCH_BTN = 0.15f;

    [Header("판정 계수(높을 수록 후한 판정)")]
    [Range(1f, 10f)]
    public float PerfectCoef = 5f;




    // >>>> 내부 컴포넌트, 오브젝트 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private RectTransform tf_rect;
    GameObject btn;



    // >>>> 내부 로직 변수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private float timer;                                // 게임 진행 타이머
    private float timer_touchInterval;                  // 터치 버튼 인터벌 타이머
    private int count_change;                           // 남은 터치 가능 횟수
    private bool bTouchBtnSpawned;                      // 터치 버튼 스폰 여부




    // >>>> 함수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        // 컴포넌트 초기화
        tf_rect = GetComponent<RectTransform>();

        // 변수 초기화
        timer = TIME_LIMIT;
        timer_touchInterval = INTERVAL_TOUCH_BTN;
        count_change = 5;
        bTouchBtnSpawned = false;
        GameStateManager.Inst.score = 0;

        // UI 초기화
        IMG_Timer.fillAmount = timer / TIME_LIMIT;
        TXT_Score.text = GameStateManager.Inst.score.ToString();
        foreach(Image img in IMG_TouchChances)
            img.sprite = SPR_ChanceAvailable;
        
        // 관련 오브젝트 활성화
        Viking.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        SpawnTouchBtn();
    }

    private void UpdateTimer()
    {
        // 타이머를 감소시키거나, 시간이 다 됐을시 결과 창으로 이동한다.
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            IMG_Timer.fillAmount = timer / TIME_LIMIT;
        }
        else
        {
            StartUI_Manager.Inst.ChangeUI(UI_Type.Finish);
        }
    }

    public void UpdateScore(int value)
    {
        GameStateManager.Inst.score += value;
        TXT_Score.text = GameStateManager.Inst.score.ToString();
    }

    public void DecreaseChance()
    {
        // 인덱스 오류 방지를 위한 경계값 체크
        if(count_change <= 0)   return;

        count_change--;
        IMG_TouchChances[count_change].sprite = SPR_ChanceUsed;
    }

    private void SpawnTouchBtn()
    {
        // 스폰 타이머를 감소시키고 리턴한다.
        if(timer_touchInterval > 0f)
        {
            timer_touchInterval -= Time.deltaTime;
            return;
        }

        // 활성화된 터치 버튼이 이미 있다면 리턴한다.
        if(bTouchBtnSpawned)    return;

        // == 터치할 버튼을 스폰한다. ==================================
        // 스폰될 위치를 가져온다(월드 좌표)
        Vector3 spawnPoint = Viking.GetRandomTouchBtnSpawnPoint();

        // 가져온 월드 좌표를 UI anchored 좌표로 변환한다.
        Vector2 anchoredPosition = WorldPositionToCanvasAnchoredPosition(spawnPoint);

        // 버튼을 스폰 시키고 터치 이벤트를 연결시킨다.
        btn = Instantiate(Prefab_TouchBtn, transform);
        btn.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        btn.GetComponent<Button>().onClick.AddListener(OnClickTouchBtn);

        // 스폰 여부를 true로 바꿔준다.
        bTouchBtnSpawned = true;
    }

    private Vector2 WorldPositionToCanvasAnchoredPosition(Vector3 worldPosition)
    {
        Vector2 anchoredPosition;

        worldPosition = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tf_rect, worldPosition, null, out anchoredPosition);

        return anchoredPosition;
    }

    public void OnClickTouchBtn()
    {
        // 버튼의 anchoredPosition을 구한다.
        Vector2 btnPos = btn.GetComponent<RectTransform>().anchoredPosition;

        // 머리와 꼬리의 월드 좌표를 anchoredPosition으로 변환한다.
        Vector2 headPos = WorldPositionToCanvasAnchoredPosition(Viking.GetHeadPosition());
        Vector2 tailPos = WorldPositionToCanvasAnchoredPosition(Viking.GetTailPosition());

        // 머리와 꼬리중 더 가까운 곳을 기준으로 점수를 올린다.
        bool bIsNearHead = true;
        float distance = 0f;
        if((headPos - btnPos).sqrMagnitude <= (tailPos - btnPos).sqrMagnitude)
        {
            // 머리가 더 가까운 경우
            bIsNearHead = true;
            distance = Vector2.Distance(headPos, btnPos) / PerfectCoef;
            GameStateManager.Inst.score += (distance < 1f) ? 1000 : (int)(1000f / distance);
        }
        else
        {
            // 꼬리가 더 가까운 경우
            bIsNearHead = false;
            distance = Vector2.Distance(tailPos, btnPos) / PerfectCoef;
            GameStateManager.Inst.score += (distance < 1f) ? 500 : (int)(500 / distance);
        }
        TXT_Score.text = GameStateManager.Inst.score.ToString();

        // === 판정에 따라 바이킹의 고도를 더욱 상승시키거나 하락시킨다. =====================================
        // 판정 계수가 높을수록 상승 판정이 까다로워진다.(이 과정을 안거치면 계수가 높을수록 상승 판정이 너무 후해진다.)
        float increaseCoef = Mathf.Lerp(6f, 1f, Mathf.InverseLerp(1f, 10f, PerfectCoef));
        if(distance / increaseCoef > 1f)
        {
            // 실패 판정이 떴을 경우. 고도를 30% 줄인다.
            Viking.SetPolarRotation(Viking.CurrentPolarRot * 0.7f);
            Debug.Log("Failed!");
        }
        else
        {
            // 성공 판정이 떴을 경우. 계산에 따라 고도를 증가시킨다.
            if(bIsNearHead)
            {
                // 머리는 최대 15, 최소 10만큼 고도가 상승한다.
                float increaseAngle = Mathf.Lerp(15f, 10f, distance / increaseCoef);
                if(distance < 1f)   increaseCoef = 15f;
                Viking.SetPolarRotation(Viking.CurrentPolarRot + increaseAngle);
                Debug.Log("Head Increase Angle: " + increaseAngle);
            }
            else
            {
                // 꼬리는 무조건 5도만큼 고도가 상승한다.
                Viking.SetPolarRotation(Viking.CurrentPolarRot + 5f);
                Debug.Log("Tail Increase: 5");
            }
        }

        // 터치 기회를 삭감한다.
        DecreaseChance();

        // 버튼 스폰 여부를 갱신하고 클릭한 버튼을 삭제한다.
        bTouchBtnSpawned = false;
        Destroy(btn);

        // 터치 기회를 다 썼다면 결과창 UI를 띄워준다.
        if(count_change == 0)
            StartUI_Manager.Inst.ChangeUI(UI_Type.Finish);
    }
}
