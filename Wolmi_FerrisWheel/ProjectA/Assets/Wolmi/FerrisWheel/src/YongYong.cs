using System;
using System.Collections;
using UnityEngine;

public class YongYong : MonoBehaviour
{
    private LineRenderer lr;
    private Rigidbody rb;
    
    private bool isDrag;
    private Vector3 firstMousePosition;
    private Vector3 throwDir;

    [SerializeField] private float power;

    private bool isCollide;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lr.enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = MouseDragCam.Instance.nearClipPlane;
        
        if (!isDrag)
        {
            lr.enabled = true;
            isDrag = true;
            
            firstMousePosition = MouseDragCam.Instance.ScreenToWorldPoint(mousePos);
        }
        else
        {
            Vector3 worldPos = MouseDragCam.Instance.ScreenToWorldPoint(mousePos);
            throwDir = firstMousePosition - worldPos;

            throwDir.z = throwDir.z < 0.0f ? 0.0f : throwDir.z;
            throwDir.y = throwDir.y < 0.0f ? 0.0f : throwDir.y;
            
            lr.SetPosition(1, throwDir);
        }
    }

    private void OnMouseUp()
    {
        if (isDrag)
        {
            lr.enabled = false;
            isDrag = false;
            
            lr.SetPosition(1, Vector3.zero);
            
            Throw();
        }
    }

    private void Throw()
    {
        rb.isKinematic = false;
        rb.AddForce(throwDir * power);
        rb.useGravity = true;
        throwDir = Vector3.zero;
    }

    private IEnumerator DisableCor()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isCollide)
        {
            Debug.Log("Call Disable Cor");
            isCollide = true;
            GameManager.Instance.CreateNewYongYong();
            StartCoroutine(DisableCor());
            enabled = false;
        }
    }
}
