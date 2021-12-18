using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class CharacterManager : MonoBehaviour
    {
        protected BumperCar bumperCar;
        protected MainGameManager mainGameManager;

        protected virtual void Awake()
        {
            bumperCar = GetComponentInChildren<BumperCar>();
            Debug.Assert(bumperCar, gameObject.name + " can't find component BumperCar");
            bumperCar.Init();

            mainGameManager = FindObjectOfType<MainGameManager>();
            Debug.Assert(mainGameManager, gameObject.name + " can't find component MainGameManager");

        }

        protected virtual void Update()
        {
            UpdatePosition();
        }

        void UpdatePosition()
        {
            Vector3 pos = bumperCar.transform.position;
            Quaternion rot = bumperCar.transform.rotation;
            transform.position = pos;
            transform.rotation = rot;
            bumperCar.transform.localPosition = Vector3.zero;
            bumperCar.transform.localRotation = Quaternion.identity;
        }

        public virtual void ExitMap()
        {
            Debug.Log(gameObject.name + " exit map");
        }

    }
}