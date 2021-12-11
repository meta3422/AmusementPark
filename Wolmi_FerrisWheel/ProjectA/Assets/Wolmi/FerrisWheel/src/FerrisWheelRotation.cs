using UnityEngine;

public class FerrisWheelRotation : MonoBehaviour
{
    private const int SitCount = 16;
    
    [SerializeField] private Transform mainWheel;
    [SerializeField] private Transform[] sitPoints;
    [SerializeField] private Transform[] sits;
    
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        if (mainWheel == null)
        {
            Debug.LogError("Please assign mainWheel Object to FerrisWheelRotation Script.");
            enabled = false;
        }

        if (sitPoints.Length != SitCount)
        {
            Debug.LogError("Please Check sitPoints at FerrisWheelRotation Script.");
            enabled = false;
        }
        
        if (sits.Length != SitCount)
        {
            Debug.LogError("Please Check sits at FerrisWheelRotation Script.");
            enabled = false;
        }
    }

    private void Update()
    {
        RotateWheel();
        SetSitsPosition();
    }

    private void RotateWheel()
    {
        mainWheel.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.right);
    }

    private void SetSitsPosition()
    {
        for (int i = 0; i < SitCount; ++i)
            sits[i].position = sitPoints[i].position;
    }
}