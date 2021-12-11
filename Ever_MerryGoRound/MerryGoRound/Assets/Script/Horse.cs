using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private const float MAXHEIGHT = 1.0f;
    private const float MINHEIGHT = 0.0f;


    private const float MAXLENGTH = 1.0f;
    private const float MINLENGTH = -0.5f;

    private Vector3 startPolePosition;
    private Vector3 startHorsePosition;
    private Vector3 startScale;

    private float signLength = 0.8f;
    private float deltaLength = 0.0f;
    private float signHeight = 1.0f;
    private float deltaHeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPolePosition = transform.GetChild(0).transform.localPosition;
        startScale = transform.GetChild(0).transform.localScale;
        startHorsePosition = transform.GetChild(1).transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLengthHorse();
        UpdateHeightHorse();
    }

    void UpdateLengthHorse()
    {
        float delta = 0.2f * Time.deltaTime * signLength;
        deltaLength += delta;

        transform.GetChild(0).transform.localPosition -= Vector3.right * delta;
        transform.GetChild(0).transform.localScale += Vector3.up * delta;
        transform.GetChild(1).transform.localPosition -= Vector3.right * delta * 2.0f;

        if (deltaLength > MAXLENGTH || deltaLength < MINLENGTH)
            signLength *= -1.0f;
    }

    void UpdateHeightHorse()
    {
        float delta = 0.1f * Time.deltaTime * signHeight;
        deltaHeight += delta;

        transform.localPosition += Vector3.up * delta;

        if (deltaHeight > MAXHEIGHT || deltaHeight < MINHEIGHT)
            signHeight *= -1.0f;
    }
}
