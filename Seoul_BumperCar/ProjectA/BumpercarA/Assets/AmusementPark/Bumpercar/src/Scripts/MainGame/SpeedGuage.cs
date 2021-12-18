using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BC.MainGame
{
    public class SpeedGuage : MonoBehaviour
    {
        Image image;
        public RectTransform rectTransform;

        public float Amount
        {
            get => image.fillAmount;
            set => image.fillAmount = Mathf.Clamp01(value);
        }

        private void Awake()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            Amount = 0;
        }

    }
}