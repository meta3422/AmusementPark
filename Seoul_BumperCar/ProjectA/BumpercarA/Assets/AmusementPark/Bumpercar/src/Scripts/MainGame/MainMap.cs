using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BC.MainGame
{
    public class MainMap : MonoBehaviour
    {
        [Header("Map Size")]
        public Vector2 size;

        [Space(0)]
        [Header("Fences")]
        public GameObject fencePrefab;
        public Vector2Int fenceNums;

        BoxCollider mapCollider;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, 0.5f, size.y));
        }

        public void Init()
        {
            var enemies = GetComponentsInChildren<EnemyManager>();
            for (int i = 0; i < enemies.Length; i++)
                enemies[i].Init();

            mapCollider = gameObject.AddComponent<BoxCollider>();
            mapCollider.isTrigger = true;
            mapCollider.size = new Vector3(size.x, 0.5f, size.y);

            // fill horizontal
            {
                Vector3 startPos = new Vector3(size.x * (1 - fenceNums.x) / (float)(2 * (1 + fenceNums.x)), 0);
                for (int i = 0; i < fenceNums.x; i++)
                {
                    var inst1 = Instantiate(fencePrefab, transform);
                    var inst2 = Instantiate(fencePrefab, transform);

                    inst1.transform.Rotate(Vector3.up * 90.0f);
                    inst2.transform.Rotate(Vector3.up * 90.0f);

                    inst1.transform.position = startPos + new Vector3(i * size.x / (float)(fenceNums.x + 1), 0, size.y * 0.5f);
                    inst2.transform.position = startPos + new Vector3(i * size.x / (float)(fenceNums.x + 1), 0, -size.y * 0.5f);
                }
            }

            // fill horizontal
            {
                Vector3 startPos = new Vector3(0, 0, size.y * (1 - fenceNums.y) / (float)(2 * (1 + fenceNums.y)));
                for (int i = 0; i < fenceNums.y; i++)
                {
                    var inst1 = Instantiate(fencePrefab, transform);
                    var inst2 = Instantiate(fencePrefab, transform);

                    inst1.transform.position = startPos + new Vector3(size.x * 0.5f, 0, i * size.y / (float)(fenceNums.y + 1));
                    inst2.transform.position = startPos + new Vector3(-size.x * 0.5f, 0, i * size.y / (float)(fenceNums.y + 1));
                }
            }
        }
    }
}