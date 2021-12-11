using UnityEngine;

public class MouseDragCam : MonoBehaviour
{
    private static Camera _instance;

    public static Camera Instance => _instance;

    private void Awake()
    {
        _instance = GetComponent<Camera>();
    }
}
