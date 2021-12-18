using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.MainGame
{
    public class MainGameManager : MonoBehaviour
    {
        MainMap map;
        PlayerManager player;
        float enemyCount;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
        }

        public void Init()
        {
            enemyCount = 0;

            map = FindObjectOfType<MainMap>();
            Debug.Assert(map, "Can't find MainMap");
            map.Init();

            player = FindObjectOfType<PlayerManager>();
            Debug.Assert(player, "Can't find InputManager");
        }

        public void CountEnemy()
        {
            enemyCount++;
        }

        public void EnemyDeeated(float point)
        {
            enemyCount--;
        }
    }
}