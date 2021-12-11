using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private const float MAXHEIGHT = 3.0f;
    private const float MINHEIGHT = 1.0f;

    private const float upSpeed = 0.8f;
    private float signHeight = 1.0f;
    private float deltaHeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
            Random.Range(MINHEIGHT, MAXHEIGHT), transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Merry_Go_Round.Instance.gameStart)
            UpdateHeightHorse();
    }

    void UpdateHeightHorse()
    {
        float delta = upSpeed * Time.deltaTime * signHeight;

        transform.localPosition += Vector3.up * delta;

        if (transform.localPosition.y > MAXHEIGHT || transform.localPosition.y < MINHEIGHT)
        {
            signHeight *= -1.0f;
            transform.localPosition -= Vector3.up * delta;
        }
    }
}
