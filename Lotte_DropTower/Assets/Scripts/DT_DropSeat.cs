using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_DropSeat : MonoBehaviour
{
    // fields
    public enum State
    {
        WaitingForRise,
        Rising,
        WaitingForDrop,
        Dropping,
        Stopped,
        Crashed,

        EnumTotal
    }
    State _state = State.WaitingForRise;

    [SerializeField]
    float _defaultDropSpeed = 100;
    [SerializeField]
    float _defaultRiseSpeed = 50;
    [SerializeField]
    float _defaultHeight = 200;

    float _currentDropSpeed = 100;
    public float DropSpeed
    { get => _currentDropSpeed;}

    float _startY;



    // unity methods
    private void Awake()
    {
        DT_GameManager.Instance.DropSeat = this;
    }

    private void Start()
    {
        _startY = transform.position.y;
    }

    private void Update()
    {
        switch(_state)
        {
            case State.WaitingForRise:
                break;

            case State.Rising:
                if (transform.position.y - _startY >= _defaultHeight)
                    ChangeState(State.WaitingForDrop);
                else
                    transform.Translate(0, _defaultRiseSpeed * Time.deltaTime, 0);
                break;

            case State.WaitingForDrop:
                break;

            case State.Dropping:
                if (_currentDropSpeed > 0.0f)
                    transform.Translate(0, -_currentDropSpeed * Time.deltaTime, 0);
                else
                    ChangeState(State.Stopped);
                break;

            case State.Stopped:
                break;
            case State.Crashed:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_state)
        {
            case State.Rising:
                if(collision.gameObject.CompareTag("Ceil"))
                {
                    ChangeState(State.WaitingForDrop);
                }
                break;
            case State.Dropping:
                if (collision.gameObject.CompareTag("Ground"))
                {
                    ChangeState(State.Crashed);
                }
                break;
        }
    }


    // original methods
    public bool ChangeState(State state)
    {
        switch(state)
        {
            case State.WaitingForRise:
                if (_state != State.Crashed && _state != State.Stopped)
                    return false;
                goto success;

            case State.Rising:
                if (_state != State.WaitingForRise)
                    return false;
                _startY = transform.position.y;
                goto success;

            case State.WaitingForDrop:
                if (_state != State.Rising)
                    return false;
                _currentDropSpeed = _defaultDropSpeed;
                goto success;

            case State.Dropping:
                if (_state != State.WaitingForDrop)
                    return false;
                goto success;

            case State.Stopped:
                if (_state != State.Dropping)
                    return false;
                goto success;

            case State.Crashed:
                if (_state != State.Dropping)
                    return false;
                goto success;

            success:
                _state = state;
                return true;

            default:
                return false;
        }
    }

    public void Brake()
    {
        float gap = transform.position.y - _startY;

        if (gap > 100)
            return;
        else if (gap > 70)
            _currentDropSpeed -= 2;
        else if(gap > 15)
            _currentDropSpeed -= 4;
        else if(gap > 1)
            _currentDropSpeed -= 8;
    }
}
