using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] GameObject swingBar;
    [SerializeField] GameObject stage;

    Vector3 originPositionOfStage;
    float swingRadius;

    float swingSpeed;
    float swingAngleMax;
    float swingPowerMinPercent;
    float spinSpeed;

    bool _changeDir;
    bool _isSwingRight;
    float _swingAngleCur;
    float _swingAngleTotal;
    float _swingPowerCur;
    

    void FixedUpdate()
    {
        SetSwingAngleCur();
        SwingBar();
        SwingStage();
        FlipStage();
        SpinStage();
        ChangeDirection();
    }

    public void InitMachine()
    {
        stage.GetComponent<BoxCollider>().center = new Vector3(0.0f , 0.22f, 0.0f);
        stage.GetComponent<BoxCollider>().size = new Vector3(10.0f , 0.4f, 10.0f);

        originPositionOfStage = stage.transform.position;
        swingRadius = (swingBar.transform.position.y - stage.transform.position.y);

        SetMachineAdjustmentValue(5.0f, 30.0f, 5.0f);
        swingPowerMinPercent = 0.3f; // min 30% will be same power 

        _changeDir = false;
        _isSwingRight = true;
        _swingAngleCur = 0.0f;
        _swingAngleTotal = 0.0f;
        _swingPowerCur = 1.0f;
    }

    public void SetMachineAdjustmentValue(float swingSpeed, float swingAngleMax, float spinSpeed)
    {
        this.swingSpeed = swingSpeed;
        this.swingAngleMax = swingAngleMax;
        this.spinSpeed = spinSpeed;
    }

    void SetSwingPower()
    {
        if (_swingAngleTotal <= swingAngleMax)
            _swingPowerCur = 1.0f - (_swingAngleTotal / swingAngleMax);
        else if (_swingAngleTotal >= swingAngleMax)
            _swingPowerCur = 1.0f - ((360.0f - _swingAngleTotal) / swingAngleMax);

        if (_swingPowerCur <= swingPowerMinPercent)
            _swingPowerCur = swingPowerMinPercent;
    }

    void SetSwingAngleCur()
    {
        //SetSwingPower();

        _swingAngleCur = swingSpeed * Time.deltaTime * _swingPowerCur;

        if (_isSwingRight)
            _swingAngleTotal += _swingAngleCur;
        else
            _swingAngleTotal -= _swingAngleCur;

        // _swingAngleTotal : 0 ~ max, 360 ~ 360-max
        if (_swingAngleTotal >= 360.0f)
            _swingAngleTotal -= 360.0f;
        else if (_swingAngleTotal <= 0.0f)
            _swingAngleTotal += 360.0f;

        // swingAngleMax <= _swingAngleTotal <= 360.0f - swingAngleMax
        if (swingAngleMax < _swingAngleTotal && _swingAngleTotal < (360.0f - swingAngleMax))
        {
            _changeDir = true;

            if (_swingAngleTotal <= 180.0f)
            {
                _swingAngleCur -= _swingAngleTotal - swingAngleMax;
                _swingAngleTotal = swingAngleMax;
            }
            else
            {
                _swingAngleCur -= (360.0f - swingAngleMax) - _swingAngleTotal;
                _swingAngleTotal = 360.0f - swingAngleMax;
            }

        }
        
    }

    void SwingBar()
    {
        // forward Axis - global
        if (_isSwingRight)
            swingBar.transform.Rotate(Vector3.left, _swingAngleCur, Space.World);
        else
            swingBar.transform.Rotate(Vector3.left, -_swingAngleCur, Space.World);
    }

    void SwingStage()
    {
        float radian = Mathf.Deg2Rad * _swingAngleTotal;

        // left dir
        float z = (Mathf.Sin(radian) * swingRadius) + originPositionOfStage.z;

        // up dir
        float y = (swingRadius - Mathf.Cos(radian) * swingRadius) + originPositionOfStage.y;
        
        stage.transform.position = new Vector3(stage.transform.position.x, y, z);
    }

    void SpinStage()
    {
        // up Axis - local 
        stage.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);
    }

    void FlipStage()
    {
        // forward Axis - global
        if (_isSwingRight)
            stage.transform.Rotate(Vector3.left, _swingAngleCur, Space.World);
        else
            stage.transform.Rotate(Vector3.left, -_swingAngleCur, Space.World);
    }

    void ChangeDirection()
    {
        if (_changeDir)
        {
            _isSwingRight = !_isSwingRight;
            _changeDir = false;
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(swingBar.transform.position, swingRadius);
    }
}
