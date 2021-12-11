using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    public void OnMovement(float vertical)
    {
        animator.SetFloat("Vertical", vertical);
    }

    public void OnRoll()
    {
        animator.SetTrigger("isRoll");
    }

    public void EndRoll()
    {
        movement.isRoll = false;
    }
}
