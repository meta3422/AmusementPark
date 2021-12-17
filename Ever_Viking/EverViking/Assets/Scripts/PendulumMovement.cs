using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    // >>>> 가져올 게임 오브젝트 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [Header("움직일 대상")]
    [Header("=============================================")]
    public GameObject MovingObj;

    [Header("바이킹의 머리와 꼬리가될 양 극점")]
    public GameObject VikingPolarLeft;
    public GameObject VikingPolarRight;

    [Header("랜덤 버튼 스폰을 위한 대체 오브젝트")]
    public GameObject BtnSpawnerObj;

    [Header("터치할 버튼이 스폰될 위치")]
    public GameObject BtnSpawnPoint1;
    public GameObject BtnSpawnPoint2;




    // >>>> 외부 조정 가능한 변수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [Header("바이킹 회전 속도")]
    [Header("=============================================")]
    public float Speed = 1f;

    [Header("초기 양극 회전값")]
    [Range(20f, 285)]
    public float FIRST_POLAR_ROTATION = 30f;




    // >>>> 내부 로직 변수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private float timer;                                // 회전 타이머
    private float leftRotX;                             // 좌극 회전값
    private float rightRotX;                            // 우극 회전값
    private float prevRotX;                             // 이전 회전값
    private float curRotX;                              // 현재 회전값
    private float currentPolarRot;                      // 현재 양극 회전값
    private bool bLeftPolarRotChanged;                  // 좌극단 회전값이 변화했는지 여부
    private bool bRightPolarRotChanged;                 // 우극단 회전값이 변화했는지 여부




    // >>>> 프로퍼티 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public float CurrentPolarRot { get { return currentPolarRot; } }




    // >>>> 함수 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        // 변수 초기화
        timer = 0f;
        currentPolarRot = FIRST_POLAR_ROTATION;
        leftRotX = -FIRST_POLAR_ROTATION;
        rightRotX = FIRST_POLAR_ROTATION;
        prevRotX = 0f;
        curRotX = 0f;
        bLeftPolarRotChanged = false;
        bRightPolarRotChanged = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        timer += Time.fixedDeltaTime;

        float t = (Mathf.Sin(timer * Speed + Mathf.PI / 2f) + 1f) / 2f;
        CheckPolarChanged(t);
        prevRotX = curRotX;
        curRotX = Mathf.Lerp(leftRotX, rightRotX, t);
        MovingObj.transform.localRotation = Quaternion.Euler(curRotX, 0f, 0f);

        //Debug.Log("t: " + t);
    }

    private void CheckPolarChanged(float t)
    {
        // 양 극이 변화하지 않았다면 리턴
        if(!bLeftPolarRotChanged && !bRightPolarRotChanged)   return;

        // 바이킹의 양 극단을 갱신한다..
        if(t < 0.01f && bRightPolarRotChanged)
        {
            rightRotX = currentPolarRot;
            bRightPolarRotChanged = false;
            Debug.Log("Right Rot X: " + rightRotX);
        }
        else if(t > 0.99f && bLeftPolarRotChanged)
        {
            leftRotX = -currentPolarRot;
            bLeftPolarRotChanged = false;
            Debug.Log("Left Rot X: " + leftRotX);
        }
    }

    public void SetPolarRotation(float rot)
    {
        currentPolarRot = rot;
        bLeftPolarRotChanged = true;
        bRightPolarRotChanged = true;
    }

    public Vector3 GetRandomTouchBtnSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        // 버튼의 유효한 랜덤 스폰 포인트를 얻기 위해 버튼 스폰용 바이킹을 임의의 위치로 회전시킨다.
        float t = UnityEngine.Random.Range(0f, 1f);
        float tempLeftRotX = -CurrentPolarRot;
        float tempRightRotX = CurrentPolarRot;
        float randomRotX = Mathf.Lerp(tempLeftRotX, tempRightRotX, t);
        BtnSpawnerObj.transform.localRotation = Quaternion.Euler(randomRotX, 0f, 0f);

        // 임의로 회전시킨 위치에서 스폰 포인트의 좌표값을 얻어온다.
        int randomSelector = UnityEngine.Random.Range(0, 2);
        switch(randomSelector)
        {
            case 0:
                spawnPoint = BtnSpawnPoint1.transform.position;
                break;
            case 1:
                spawnPoint = BtnSpawnPoint2.transform.position;
                break;
        }

        return spawnPoint;
    }

    public Vector3 GetHeadPosition()
    {
        return (curRotX - prevRotX > 0) ? 
            VikingPolarLeft.transform.position : VikingPolarRight.transform.position;
    }

    public Vector3 GetTailPosition()
    {
        return (curRotX - prevRotX > 0) ? 
            VikingPolarRight.transform.position : VikingPolarLeft.transform.position;
    }
}
