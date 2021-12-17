using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "ShipSquare")
            Destroy(this.gameObject);

    }
}
