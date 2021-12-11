using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("MoveSpeed")]
    [SerializeField]
    private float moveSpeed;
    private float rotateAngle;
    private float rotateSpeed = 10.0f;

    public Vector3 moveDirection;
    public Vector3 rollDirection;

    public bool isRoll;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
        moveDirection.Normalize();
        moveDirection.y = 0.0f;

        if (isRoll)
            moveDirection = rollDirection;

        moveDirection *= moveSpeed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, Vector3.zero);
        projectedVelocity.y -= 9.81f;
        characterController.Move(projectedVelocity * Time.deltaTime);
    }

    public void Rotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir.z = moveDirection.z;
        targetDir.x += moveDirection.x;
        targetDir.Normalize();
        targetDir.y = 0.0f;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotateSpeed * Time.deltaTime);

        transform.rotation = targetRotation;
    }

    public void Roll(Vector3 direction)
    {
        rollDirection = Vector3.zero;
        rollDirection.z = direction.z;
        rollDirection.x += direction.x;
        rollDirection.Normalize();
        rollDirection.y = 0.0f;

        Quaternion rollRotation = Quaternion.LookRotation(rollDirection);
        transform.rotation = rollRotation;
    }
}
