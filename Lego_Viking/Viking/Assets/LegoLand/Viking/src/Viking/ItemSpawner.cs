using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
   // public enum Mode { item, missiles }
  //  public Mode mode = Mode.item;

    public GameObject[] Instances;
    public Transform[] SpawnPoint;
    public float Interval = 5.0f;
    public int Lifetime = 5;
    public int InstanceCount = 10;
    public float Range = 5.0f;

    IEnumerator Start()
    {
        for (int i = 0; i < InstanceCount; i++)
        {
            InstantiateItems();
            yield return new WaitForSeconds(Interval);
        }
        
        
    }






    private void InstantiateItems()
    {
        //
        {
            int posIndex = Random.Range(0, SpawnPoint.Length);
            GameObject obj = Instantiate(
                                                          Instances[Random.Range(0, Instances.Length)],
                                                         SpawnPoint[posIndex].position,
                                                         this.transform.rotation);
            obj.transform.parent = SpawnPoint[posIndex];
            Destroy(obj, Lifetime);
        }  
    }

  
}
