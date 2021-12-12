using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Lobby,
    InGame,
    End
}


public class GameManager : MonoBehaviour
{
    State state;

    [SerializeField] MachineController machine;
    [SerializeField] PlayerController player;
    [SerializeField] UIController ui;

    KeyManager keyManager;
    TimeManager timeManager;

    int _monsterCur = 0;

    // ******** Control Values Here **********
    // monster count
    int monsterMax = 9;

    // limit time
    int timeMax = 180;

    // machine
    float swingSpeed = 5.0f;
    float swingAngleMax = 60.0f;
    float spinSpeed = 15.0f;

    // player
    float gravity = 1.0f;
    float moveSpeed = 2.0f;
    float rotSpeed = 130.0f;
    float jumpPower = 5.0f;
    // ******** Control Values Here **********

    public bool IsMachineStopped { get { return machine.IsStopped(); } }


    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;        
    }

    void Start()
    {
        InitInGame();
        StartInGame();
    }

    void Update()
    {
        UpdateInGame();
    }

    void InitInGame()
    {
        ChangeGameState(State.InGame);

        keyManager = new KeyManager();
        timeManager = new TimeManager();

        machine.InitMachine(swingSpeed, swingAngleMax, spinSpeed);
        player.InitPlayer(keyManager, gravity, moveSpeed, rotSpeed, jumpPower, spinSpeed);
        ui.InitUI(timeMax, monsterMax);
    }

    void StartInGame()
    {
        timeManager.StartTimer();
        machine.StartMoving();
        player.StartMoving();
    }

    void UpdateInGame()
    {
        if (state == State.InGame)
        {
            ui.UpdateTime(timeManager.GetCurrentTime());
        }
    }

    public void SetGameOver()
    {
        ChangeGameState(State.End);
        player.StopMoving();
        machine.StopMoving();
    }

    public void SetWin()
    {
        ChangeGameState(State.End);
        player.PauseMoving();
        machine.PauseMoving();
    }

    public void SetPause()
    {
        player.PauseMoving();
        machine.PauseMoving();
    }

    public void KilledMonster()
    {
        _monsterCur--;
        ui.UpdateMonsterCount(_monsterCur);

        if (_monsterCur == 0)
            SetWin();
    }

    void ChangeGameState(State state)
    {
        this.state = state;
    }

}