using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode rotateLeft = KeyCode.A;
    public KeyCode rotateRight = KeyCode.D;

    public bool LeftKey { get; private set; }
    public bool rightKey { get; private set; }


    private static InputManager sInstance;

    public static InputManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_InputManager");
                sInstance = newGameObj.AddComponent<InputManager>();
                DontDestroyOnLoad(newGameObj);
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

    void Update()
    {
        LeftKey = Input.GetKey(rotateLeft);
        rightKey = Input.GetKey(rotateRight);
    }
}
