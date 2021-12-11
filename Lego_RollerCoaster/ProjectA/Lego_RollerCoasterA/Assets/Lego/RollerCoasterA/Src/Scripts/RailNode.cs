using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailNode : MonoBehaviour
{
    [SerializeField]
    RailNode Prev = null;
    [SerializeField]
    public RailNode Next = null;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }

    
}
