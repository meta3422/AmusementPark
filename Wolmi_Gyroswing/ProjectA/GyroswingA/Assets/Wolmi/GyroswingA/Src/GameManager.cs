using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MachineController machine;
    [SerializeField] PlayerController player;
    [SerializeField] UIController ui;

    KeyManager keyManager;
    TimeManager timeManager;

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
        keyManager = new KeyManager();
        timeManager = new TimeManager();

        machine.InitMachine();
        player.InitPlayer(keyManager);
        ui.InitUI(3,9);
    }

    void StartInGame()
    {
        timeManager.StartTimer();
    }

    void UpdateInGame()
    {
        ui.UpdateTime(timeManager.GetCurrentTime());
    }

}