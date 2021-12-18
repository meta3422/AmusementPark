using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class MainGameUI : MonoBehaviour
    {
        SpeedGuage speedGuage;

        private void Awake()
        {
            speedGuage = GetComponentInChildren<SpeedGuage>();
            if (speedGuage) speedGuage.gameObject.SetActive(false);
        }

        public void ShowSpeedGuage(Vector3 pos)
        {
            Vector2 proj = Camera.main.WorldToScreenPoint(pos);
            speedGuage.gameObject.SetActive(true);
            speedGuage.rectTransform.anchoredPosition = proj;
        }

        public void HideSpeedGuage()
        {
            speedGuage.gameObject.SetActive(false);
        }

        public void SetSpeedGuage(float amount)
        {
            speedGuage.Amount = amount;
        }
    }
}