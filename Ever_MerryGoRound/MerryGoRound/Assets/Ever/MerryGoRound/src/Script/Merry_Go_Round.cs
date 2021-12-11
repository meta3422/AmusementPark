using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merry_Go_Round : MonoBehaviour
{
    private static Merry_Go_Round sInstance;

    public static Merry_Go_Round Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_Merry_Go_Round");
                sInstance = newGameObj.AddComponent<Merry_Go_Round>();
            }

            return sInstance;
        }
    }
    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
    }

    private const float originRotationSpeed = -72.0f;
    public float rotationSpeed = -originRotationSpeed;
    [SerializeField] private float deltaRotation = 0.0f;
    private float speedmultiples = 1.1f;
    public int lapse = 0;
    private const int perLapse = 3;
    public GameObject Playerhorse;

    public bool gameStart = false;

    public float Record = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        if (Merry_Go_Round.Instance.gameStart)
        {
            rotateMerry_Go_Round();
            UpdateRecord();
        }
    }

    void rotateMerry_Go_Round()
    {
        float deltaRot = rotationSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0.0f, deltaRot, 0.0f);
        deltaRotation += deltaRot;
        CheckLapse();
    }

    void CheckLapse()
    {
        if (deltaRotation < -360)
        {
            deltaRotation += 360;
            lapse++;
            ChangeSpeed();
        }
    }
    void ChangeSpeed()
    {
        if (lapse % perLapse == 0)
        {
            rotationSpeed *= speedmultiples;
        }
    }

    void UpdateRecord()
    {
        Record += Time.deltaTime * -rotationSpeed / 6.0f;
    }

    public void ResetGameSetting()
    {
        Playerhorse.transform.localRotation = Playerhorse.GetComponent<HorseController>().originRotaion;

        rotationSpeed = originRotationSpeed;
        deltaRotation = 0.0f;
        lapse = 0;
        Record = 0.0f;
    }

    public float GetLevelPower()
    {
        return rotationSpeed / originRotationSpeed;
    }
}
