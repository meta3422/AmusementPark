using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField] private float Hold = 1.0f;
    private const float holdPower = 0.05f;
    public Quaternion originRotaion;
    

    private float checkTime;

    void Start()
    {
        originRotaion = transform.localRotation;
    }

    void Update()
    {
        if (Merry_Go_Round.Instance.gameStart)
        {
            ControlHorse();
            CheckGameOver();
        }
    }

    void FixedUpdate()
    {
        if (Merry_Go_Round.Instance.gameStart)
            PhysicsAction();
    }

    void ControlHorse()
    {
        if (InputManager.Instance.LeftKey)
        {
            transform.rotation *= Quaternion.Euler(30.0f * Time.deltaTime * Hold, 0.0f, 0.0f);
            Hold += holdPower;
        }
        else if (InputManager.Instance.rightKey)
        {
            transform.rotation *= Quaternion.Euler(-30.0f * Time.deltaTime * Hold, 0.0f, 0.0f);
            Hold += holdPower;
        }
        else
        {
            Hold = 1.0f;
        }
    }

    void PhysicsAction()
    {
        transform.localRotation *= Quaternion.Euler(-Merry_Go_Round.Instance.GetLevelPower() * 10.0f * Time.deltaTime, 0.0f, 0.0f);
        
        if (transform.localRotation.x < 0)
        {
            transform.localRotation *= Quaternion.Euler(transform.localRotation.x * 180.0f * Time.deltaTime, 0.0f, 0.0f);
        }
        else if (transform.localRotation.x > 0)
        {
            transform.localRotation *= Quaternion.Euler(transform.localRotation.x * 180.0f * Time.deltaTime, 0.0f, 0.0f);
        }
    }
    void CheckGameOver()
    {
        if (transform.localRotation.x > 0.95f || transform.localRotation.x < -0.95f)
        {
            Merry_Go_Round.Instance.gameStart = false;
            GameoverUI.ActiveGameOverTextTrue();
        }

        if (transform.localRotation.x > 0.70f || transform.localRotation.x < -0.70f)
        {
            checkTime += Time.deltaTime;
            if(checkTime > 1.5f)
            {
                Merry_Go_Round.Instance.gameStart = false;
                GameoverUI.ActiveGameOverTextTrue();
            }
        }
        else
        {
            checkTime = 0.0f;
        }
    }
}
