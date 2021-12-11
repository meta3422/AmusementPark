using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;

    private Movement movement;
    private PlayerAnimator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Roll();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌,우 움직임
        float z = Input.GetAxisRaw("Vertical"); // 위, 아래 움직임

        direction = new Vector3(x, 0, z);

        movement.MoveTo(direction);
        
        movement.Rotation();

        animator.OnMovement(Mathf.Clamp01(Mathf.Abs(x) + Mathf.Abs(z)));
    }

    private void Roll()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !movement.isRoll)
        {
            movement.isRoll = true;
            movement.Roll(direction);
            animator.OnRoll();
        }
    }
}
