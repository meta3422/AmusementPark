using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject[] Missiles;
    public float Range = 5.0f;
    public float Interval;
    public int Lifetime = 5;

    IEnumerator Start() //일정시간마다 실행
    {
        while (true)
        {
            transform.position = new Vector3( Random.Range(-Range, Range)
                                           , transform.position.y,
                                            transform.position.z);
            GameObject obj = Instantiate(
                                                        Missiles[Random.Range(0,Missiles.Length)], 
                                                        transform.position,
                                                        Quaternion.Euler(90.0f,0,0)); //object 생성
            
            Destroy(obj, Lifetime);
            yield return new WaitForSeconds(Interval); //interval 후에 다시 호출
        }
    }


}
