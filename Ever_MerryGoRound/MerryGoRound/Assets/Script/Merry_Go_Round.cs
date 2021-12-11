using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merry_Go_Round : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateMerry_Go_Round();
    }

    void rotateMerry_Go_Round()
    {
        this.transform.rotation *= Quaternion.Euler(0.0f, 30.0f * Time.deltaTime, 0.0f);
    }
}
