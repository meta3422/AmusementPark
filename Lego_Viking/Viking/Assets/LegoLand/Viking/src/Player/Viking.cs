using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VikingMode { LimitlessMode, LimitMode }

public class Viking : MonoBehaviour
{
    // 바이킹 작동 원리
    //바이킹 역시 기계적인 힘은 필요하다.배 밑에 모터가 연결된 롤러가 있는데 왕복 시킬 때마다 시간에 맞춰 배의 밑 부분을 밀어주는 것이다.
    // https://srdeveloper.tistory.com/41
    //https://www.youtube.com/watch?v=Q75C0kZhu80



    //  ===================== 바이킹 =======================
    public VikingMode mode = VikingMode.LimitlessMode;
   [HideInInspector] public GameObject wheel;
    private Rigidbody WheelRigidbody;
    public bool bStop = false;
    public float LimitModeTime = 100;
    public float timer = 0;

    //  ===================== 물리 =======================
    public float Speed = 50.0f;
    public float limitAngle = 30.0f;
    Quaternion start, end;


    void Start()
    {
        timer = 0.0f;
       // WheelRigidbody = wheel.GetComponent<Rigidbody>();
        start= PendulumRotation (limitAngle);
        end = PendulumRotation (-limitAngle);
    }

    private void FixedUpdate()
    {
        RunTimer();
        if (timer > LimitModeTime)
        {
            if (Speed <= 1)
            {
                Speed = 1;
               // bStop = true;
            }
            else Speed -= Time.deltaTime;
        }
        
        if (Mathf.CeilToInt(wheel.transform.localEulerAngles.x) >= limitAngle)
        {
            Debug.Log(" 고점 : " + wheel.transform.rotation.x);
            start = PendulumRotation((limitAngle-5));
            end = PendulumRotation(-(limitAngle-5));
        }
         if (!bStop) wheel.transform.rotation = Quaternion.Lerp (start, end, (Mathf.Sin(timer * (Speed/5.0f) + Mathf.PI/2)+1.0f)/2.0f);
        // if (!bStop) wheel.transform.rotation = Quaternion.Lerp (start, end, (Mathf.Sin(timer * Speed)+1.0f)/2.0f);

    }

    private void RunTimer()
    {
        timer += Time.deltaTime;
    }

    private void ResetTimer()
    {
        timer = 0.0f;
    }

    Quaternion PendulumRotation(float angle)
    {
        Quaternion pendulumRotation = wheel.transform.rotation;
        float angleX = pendulumRotation.eulerAngles.x + angle;

        if (angleX > limitAngle) angleX -= 360;
        else if (angleX < -limitAngle) angleX += 360;

        pendulumRotation.eulerAngles = new Vector3(angleX, pendulumRotation.eulerAngles.y, pendulumRotation.eulerAngles.z);

        return pendulumRotation;
    }

}
