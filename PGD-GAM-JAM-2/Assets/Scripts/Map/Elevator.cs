using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public enum ElevatorStates
    {
        Idle,
        TeleportToMenu,
        TeleportToGame,
        WaitingToMenu,
        WaitingToGame
    }
    public ElevatorStates CurrentElevatorState;

    public GameObject Player;

    public Transform ElevatorPosMenu;
    public Transform ElevatorPosGame;

    public GameObject LeftDoor;
    public GameObject RightDoor;

    public Rigidbody ElevatorButton;

    private bool GoToGame = true;

    private float ElevatorTime = 5;
    private float ElevatorTimer;

    private FMODUnity.StudioEventEmitter AudioEmitter;

    // Start is called before the first frame update
    void Start()
    {
        AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        Player = GameObject.FindGameObjectWithTag("Player");
        ElevatorTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Player.transform.position = ElevatorPosMenu.position;
        switch (CurrentElevatorState)
        {
            case ElevatorStates.Idle:
                break;

            case ElevatorStates.TeleportToMenu:
                transform.position = ElevatorPosMenu.position;
                Player.transform.position = ElevatorPosMenu.position;

                ElevatorButton.AddRelativeForce(Vector3.down * 10);

                CurrentElevatorState = ElevatorStates.Idle;
                break;

            case ElevatorStates.TeleportToGame:
                transform.position = ElevatorPosGame.position;
                Player.transform.position = ElevatorPosGame.position;

                ElevatorButton.AddRelativeForce(Vector3.down * 10);

                CurrentElevatorState = ElevatorStates.Idle;
                break;

            case ElevatorStates.WaitingToMenu:
                if (ElevatorTimer < ElevatorTime)
                {
                    ElevatorTimer += Time.deltaTime;
                }
                else
                {
                    CurrentElevatorState = ElevatorStates.TeleportToMenu;
                    ElevatorTimer = 0;
                }
                break;

            case ElevatorStates.WaitingToGame:
                if (ElevatorTimer < ElevatorTime)
                {
                    ElevatorTimer += Time.deltaTime;
                }
                else
                {
                    CurrentElevatorState = ElevatorStates.TeleportToGame;
                    ElevatorTimer = 0;
                }
                break;
        }
    }

    public void MoveElevator()
    {
        if (CurrentElevatorState == ElevatorStates.Idle)
        {
            ElevatorTimer = 0;

            if (GoToGame)
            {
                CurrentElevatorState = ElevatorStates.WaitingToGame;
            }
            else
            {
                CurrentElevatorState = ElevatorStates.WaitingToMenu;
            }
        }
    }
}
