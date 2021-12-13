using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestViking : MonoBehaviour
{
    // 앞머리에 bool 
    // 가운데를 지나는 끝의 반대편에 가속

        // 힘
        // 스피드
        // 중력
        // 

      
    public float angle = 0;
    private float lerpTimer = 0;
    public float speed = 2f;
    public float Addedforce = 0f;
    public float WheelForce = 0f;
    public float gravity = 0.0f;
    public float durationValue = 30.0f;
    public Collider Wheel;

    public bool bFoece = false;

    private void FixedUpdate()
    {
         lerpTimer += Time.deltaTime * (speed + WheelForce) / durationValue;
         //lerpTimer += Time.deltaTime * (speed ) / 20;
        transform.rotation = PendulumRotation();
      
        lerpTimer += Time.deltaTime * (speed+ WheelForce) / durationValue;
        transform.rotation = PendulumRotation();

        if (speed > 0.0f) speed -= Time.deltaTime * gravity;
        else speed = 0;

        
        if (angle > 0.0f) angle -= Time.deltaTime;
        else angle = 0;


        WheelForce = Addedforce;
        Addedforce = 0;

        if (WheelForce != 0)
        {
            speed += WheelForce;
            WheelForce = 0;
        }


    }

  
   

    Quaternion PendulumRotation()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle), Quaternion.Euler(Vector3.back * angle), ((Mathf.Sin(lerpTimer) + 1.0f) * 0.5f));
    }

}
