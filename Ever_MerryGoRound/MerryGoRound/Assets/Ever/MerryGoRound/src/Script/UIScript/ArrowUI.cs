using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    public RectTransform arrow;

    [SerializeField] private GameObject playerHorse;

    void Update()
    {
        if(Merry_Go_Round.Instance.gameStart)
            SetArrow();
    }

    void SetArrow()
    {
        float anglex = playerHorse.transform.localEulerAngles.x;

        if (playerHorse.transform.localRotation.x < 0)
        {
            arrow.localPosition = new Vector3(-playerHorse.transform.localRotation.x * 300.0f,
                arrow.localPosition.y,
                arrow.localPosition.z);
        }
        else
        {
            arrow.localPosition = new Vector3(-playerHorse.transform.localRotation.x * 300.0f,
                arrow.localPosition.y,
                arrow.localPosition.z);
        }
    }

}
