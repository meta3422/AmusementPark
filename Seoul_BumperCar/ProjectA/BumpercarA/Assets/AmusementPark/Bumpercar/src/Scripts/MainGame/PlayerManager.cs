using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class PlayerManager : CharacterManager
    {
        enum State { Idle, OnMove, Aimming }

        public float maxSpeed = 70;
        public float minSpeed = 20;
        public float maxAimmingTime = 4.0f;
        
        InputManager inputManager;
        MainGameUI mainGameUI;
        State state;
        Vector2 aimStartPos;
        Vector3 aimDirection;

        float _time;

        protected override void Awake()
        {
            base.Awake();
            inputManager = GetComponent<InputManager>();
            Debug.Assert(bumperCar, "Player Manager can't find component InputManager");

            mainGameUI = FindObjectOfType<MainGameUI>();
            Debug.Assert(mainGameUI, gameObject.name + "Can't find MainGameUI");

            state = State.Idle;
        }

        protected override void Update()
        {
            base.Update();
            UpdateState();
        }

        void UpdateState()
        {
            switch(state)
            {
                case State.Aimming:
                    UpdateAimmingState();
                    break;

                case State.OnMove:
                    UpdateOnMoveState();
                    break;
            }
        }

        void UpdateAimmingState()
        {
            // update guage ui

            Vector2 rel = aimStartPos - inputManager.mousePos;
            if (rel.magnitude > 1)
            {
                aimDirection.x = rel.x;
                aimDirection.y = 0;
                aimDirection.z = rel.y;
                aimDirection.Normalize();
            }
            transform.rotation = Quaternion.LookRotation(aimDirection.normalized);

            _time += Time.deltaTime;
            mainGameUI.SetSpeedGuage(_time > maxAimmingTime ? 1.0f : _time / maxAimmingTime);
        }

        void UpdateOnMoveState()
        {
            if (bumperCar.IsMoving) return;
            state = State.Idle;
        }

        public void StartAimming()
        {
            if (state != State.Idle) return;
            state = State.Aimming;
#if UNITY_EDITOR
            aimStartPos = inputManager.mousePos;
#else

#endif
            aimDirection = transform.forward;
            mainGameUI.ShowSpeedGuage(transform.position + 2.0f * Vector3.right);
            _time = 0;
        }

        public void ReleaseAimming()
        {
            if (state != State.Aimming) return;
            state = State.OnMove;
            // hide guage UI;
            mainGameUI.HideSpeedGuage();
            bumperCar.Velocity = transform.forward * (_time < maxAimmingTime ? minSpeed + _time * (maxSpeed - minSpeed) / maxAimmingTime : maxSpeed);
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log(other.gameObject);
        }

    }
}