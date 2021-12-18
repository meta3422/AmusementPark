using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class BumperCar : MonoBehaviour
    {
        Rigidbody rigidBody;
        CharacterManager characterManager;

        public bool IsStopped { get => rigidBody.velocity.magnitude <= 1.0e-3f; }
        public bool IsMoving { get => rigidBody.velocity.magnitude > 1.0e-3f; }

        public Vector3 Velocity
        {
            get => rigidBody.velocity;
            set => rigidBody.velocity = value;
        }

        public void Init()
        {
            rigidBody = GetComponent<Rigidbody>();
            characterManager = GetComponentInParent<CharacterManager>();
        }

        private void OnTriggerExit(Collider other)
        {
            characterManager.ExitMap();
        }
    }
}