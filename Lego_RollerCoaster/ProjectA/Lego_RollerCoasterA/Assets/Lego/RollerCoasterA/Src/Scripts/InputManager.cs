using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public enum Type { Tap, Pressed, Slide }

    //UnityEvent[] touchEvents = new UnityEvent[System.Enum.GetValues(typeof(Type)).Length];

    Touch touch;
    DeviceType device;

    void Start()
    {
        device = SystemInfo.deviceType;
    }


    void Update()
    {
        switch (device)
        {
            case DeviceType.Handheld:
                InputTouch();
                break;
            case DeviceType.Desktop:
                InputMouse();
                break;
        }
    }

    void InputTouch()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Enemy"))
                    {
                        raycastHit.collider.GetComponent<EnemyMediator>().Die();
                    }
                }
            }
        }
    }

    void InputMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Enemy"))
                {
                    raycastHit.collider.GetComponent<EnemyMediator>().Die();
                }



            }
        }
        
    }

    //bool GetMouseDown()
    //{


    //    return false;
    //}

}
