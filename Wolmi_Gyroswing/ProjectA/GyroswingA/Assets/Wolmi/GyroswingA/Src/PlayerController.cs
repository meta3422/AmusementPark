using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingThings
{
    [SerializeField] LayerMask stageLayer;
    [SerializeField] LayerMask failZoneLayer;

    KeyManager key;
    Rigidbody rb;

    float rayDistance = 10.0f;
    RaycastHit hit;

    float gravityPower;

    float moveSpeed;
    float rotSpeed;
    float jumpPower;

    [SerializeField] GameObject stage;
    float spinSpeed;

    bool _isJumping;
    bool _isOnStage;

    void FixedUpdate()
    {
        if (!IsPaused())
        {
            if (!IsStopped())
            {
                MovePlayer();
                TurnPlayer();
                JumpPlayer();
            }

            SpinPlayerAlongStage();
            AffectedByCentrifugalForce();
            StandUp();
        }
    }

    public void InitPlayer(KeyManager keyManager, float gravityPower,
                            float moveSpeed, float rotSpeed, float jumpPower, 
                            float spinSpeed)
    {
        key = keyManager;
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = true;
        this.gravityPower = gravityPower;

        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.jumpPower = jumpPower;
        SetSpinSpeed(spinSpeed);

        _isJumping = false;
        _isOnStage = true;

        InitMovingThings();
    }    

    public void SetSpinSpeed(float spinSpeed)
    {
        this.spinSpeed = spinSpeed;
    }

    void MovePlayer()
    {        
        if (!_isJumping)
        {
            Vector3 dir = transform.forward * key.GetVerticalKey();

            dir.Normalize();

            transform.position += dir * moveSpeed * Time.deltaTime;
        }
    }

    void TurnPlayer()
    {
        float angle = key.GetHorizontalKey() * rotSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, angle);
    }

    void JumpPlayer()
    {
        if (key.IsJumpKeyPressed() && !_isJumping)
        {
            _isJumping = true;
            _isOnStage = false;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void SpinPlayerAlongStage()
    {
        if (_isOnStage && !GameManager.Instance.IsMachineStopped)
        {
            transform.RotateAround(stage.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
        }
    }

    void AffectedByCentrifugalForce()
    {
        if (!_isJumping)
        {
            if (_isOnStage)
            {
                rb.AddForce(-stage.transform.up * gravityPower, ForceMode.Impulse);
            }
            //else
            //{
            //    rb.AddForce(-Vector3.up * gravityPower, ForceMode.Impulse);
            //}
        }
    }

    void StandUp()
    {
        //Debug.DrawRay(transform.position, direction * rayDistance, Color.blue, 0.3f);

        //// this guy never falls down when standing on the stage
        //if (_isOnStage)
        //{
        //    if (Physics.Raycast(transform.position, direction, out hit, rayDistance, stageLayer))
        //    {

        //    }
        //}
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == stageLayer.value)
        {
            _isJumping = false;
            _isOnStage = true;
        }
        else if ((1 << collision.gameObject.layer) == failZoneLayer.value)
        { 
            Debug.Log("You fell. Game over.");
            _isJumping = false;
            _isOnStage = false;
        }
    }

}
