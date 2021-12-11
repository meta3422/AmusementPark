using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMediator : MonoBehaviour
{
    
    public void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
    }

}
