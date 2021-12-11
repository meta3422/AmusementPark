using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    [SerializeField]
    Vector3 Direction = Vector3.forward;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Direction * 500.0f);
    }

    void Update()
    {
        transform.Translate(Direction * 1.0f * Time.deltaTime);
        //if(Input.GetKeyDown(KeyCode.W))
        //    GetComponent<Rigidbody>().AddForce(Direction * 500.0f);
        //if (Input.GetKeyDown(KeyCode.S))
        //    GetComponent<Rigidbody>().AddForce(-Direction * 500.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Rail"))
        {
            transform.LookAt(other.GetComponent<RailNode>().Next.transform);
        }
    }
}
