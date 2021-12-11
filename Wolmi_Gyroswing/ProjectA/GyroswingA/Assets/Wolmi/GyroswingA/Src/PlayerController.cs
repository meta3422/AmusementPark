using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask failLayer;

    KeyManager key;
    Rigidbody rb;

    float moveSpeed;
    float rotSpeed;
    float jumpPower;

    bool _isJumping;

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        JumpPlayer();
    }

    public void InitPlayer(KeyManager keyManager)
    {
        key = keyManager;
        rb = GetComponent<Rigidbody>();

        moveSpeed = 2.0f;
        rotSpeed = 150.0f;
        jumpPower = 4.0f;
    }    

    void MovePlayer()
    {        
        Vector3 dir = transform.forward * key.GetVerticalKey();

        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void RotatePlayer()
    {
        float angle = key.GetHorizontalKey() * rotSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, angle);
    }

    void JumpPlayer()
    {
        if (key.IsJumpKeyPressed() && !_isJumping)
        {
            _isJumping = true;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == groundLayer.value && _isJumping)
        {
            //Debug.Log("To stage");
            _isJumping = false;
        }
        else if ((1 << collision.gameObject.layer) == failLayer.value)
        {
            //Debug.Log("Game over. You touched the ground");

        }
    }

}
