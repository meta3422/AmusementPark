using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class InputManager : MonoBehaviour
    {
        PlayerManager player;
        public Vector2 mousePos { get; private set; }

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            TickInput();
        }

        public void TickInput()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) player.StartAimming();
            if (Input.GetMouseButtonUp(0)) player.ReleaseAimming();

            mousePos = Input.mousePosition;
#else
            if (Input.touchCount <= 0) return;

#endif
        }

    }
}
