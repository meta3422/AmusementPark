using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    private Vector3 direction;
    private Movement movement;
    private PlayerAnimator animator;

    #region Input variables
    // #. Keyboard
    private float hAxis;
    private float vAxis;

    // #. Joypad(KeyPad)
    [HideInInspector]
    public bool[] keyControl = new bool[9];
    [HideInInspector]
    public bool isControl;
    [HideInInspector]
    public bool isButtonRoll;
    [HideInInspector]
    public bool isButtonFire;
    [HideInInspector]
    public bool isButtonReload;
    [HideInInspector]
    public bool isButtonGrenade;
    #endregion

    #region Weapon variables
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public bool isReload;
    float fireDelay;
    bool isFireReady;
    public int ammo;

    [Header("Grenade")]
    public GameObject grenadeObj;
    public Transform grenadePos;
    public int hasGrenades;
    public float throwPower;
    public float throwHeight;
    #endregion

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<PlayerAnimator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Roll();
        Fire();
        Reload();
        Grenade();
    }

    #region Input

    private void GetInput()
    {
        // #. Keypad Control
        if (keyControl[0]) { hAxis = -1; vAxis = 1; }
        if (keyControl[1]) { hAxis = 0; vAxis = 1; }
        if (keyControl[2]) { hAxis = 1; vAxis = 1; }
        if (keyControl[3]) { hAxis = -1; vAxis = 0; }
        if (keyControl[4]) { hAxis = 0; vAxis = 0; }
        if (keyControl[5]) { hAxis = 1; vAxis = 0; }
        if (keyControl[6]) { hAxis = -1; vAxis = -1; }
        if (keyControl[7]) { hAxis = 0; vAxis = -1; }
        if (keyControl[8]) { hAxis = 1; vAxis = -1; }
        if (!isControl) { hAxis = 0; vAxis = 0; }

        // #. KeyBoard Control
        hAxis = Input.GetAxisRaw("Horizontal"); // 좌,우 움직임
        vAxis = Input.GetAxisRaw("Vertical"); // 위, 아래 움직임

        isButtonRoll = Input.GetKeyDown(KeyCode.LeftShift);
        isButtonFire = Input.GetButton("Fire1");
        isButtonReload = Input.GetKeyDown(KeyCode.R);
        isButtonGrenade = Input.GetKeyDown(KeyCode.G);
    }

    #endregion

    #region Mobile KeyPad

    public void KeyPad(int type)
    {
        for(int i = 0; i < 9; i++)
        {
            keyControl[i] = i == type;
        }
    }

    public void KeyDown()
    {
        isControl = true;
    }

    public void KeyUp()
    {
        isControl = false;
    }

    public void ButtonRollDown()
    {
        if(!movement.isRoll)
            isButtonRoll = true;
    }

    public void ButtonFireDown()
    {
        isButtonFire = true;
    }

    public void ButtonFireUp()
    {
        isButtonFire = false;
    }

    public void ButtonReload()
    {
        isButtonReload = true;
    }

    public void ButtonGrenadeDown()
    {
        isButtonGrenade = true;
    }

    #endregion

    #region Movement & Roll

    private void Move()
    {
        direction = new Vector3(hAxis, 0, vAxis);

        movement.MoveTo(direction);
        movement.Rotation();
        animator.OnMovement(Mathf.Clamp01(Mathf.Abs(hAxis) + Mathf.Abs(vAxis)));
    }

    private void Roll()
    {
        if(isButtonRoll && !movement.isRoll && !isButtonReload)
        {
            movement.isRoll = true;
            movement.Roll(direction);
            animator.OnRoll();
        }
    }

    #endregion

    #region Action

    private void Fire()
    {
        if (weapon == null) return;

        fireDelay += Time.deltaTime;
        isFireReady = weapon.rate < fireDelay;

        if(isButtonFire && isFireReady && !isButtonRoll && !isReload)
        {
            weapon.Use();
            animator.OnFire();
            fireDelay = 0;
        }
    }

    private void Reload()
    {
        if (weapon == null) return;

        if (ammo == 0) return;

        if(isButtonReload && !isButtonRoll && isFireReady && !isReload)
        {
            isReload = true;
            animator.OnReload();
        }
    }

    private void Grenade()
    {
        if (hasGrenades == 0) return;

        if(isButtonGrenade && !isReload)
        {
            GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
            Vector3 throwVec = transform.forward * throwPower;
            throwVec.y = throwHeight;
            
            rigidGrenade.AddForce(throwVec, ForceMode.Impulse);
            rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            hasGrenades--;
        }

    }

    #endregion
}
