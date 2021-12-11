using System;
using UnityEngine;

public class YongYong : MonoBehaviour
{
    private LineRenderer lr;
    
    private bool isDrag;
    private Vector3 firstMousePosition;
    
    [SerializeField] private Vector3 throwDir;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
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
            
            throwDir = Vector3.zero;
        }
    }
}
