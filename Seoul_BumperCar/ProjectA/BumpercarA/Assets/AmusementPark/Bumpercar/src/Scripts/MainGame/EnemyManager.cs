using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class EnemyManager : CharacterManager
    {
        public void Init()
        {
            mainGameManager.CountEnemy();
        }

        public override void ExitMap()
        {
            mainGameManager.EnemyDeeated(1);
            var collider = GetComponentInChildren<Collider>();
            collider.enabled = false;
            Destroy(gameObject, 3.0f);
        }
    }
}