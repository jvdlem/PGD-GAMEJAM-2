using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Rigidbody ElevatorButton;

    public FMODUnity.StudioEventEmitter AudioEmitter;

    public bool PlayerInElevator, Done;

    float elevatorTimer = 2;

    private void Update()
    {
        elevatorTimer -= Time.deltaTime;
        if (elevatorTimer <= 0 && !Done)
        {
            ElevatorButton.AddRelativeForce(Vector3.down * 15);
            AudioEmitter.Play();
            elevatorTimer = 5;
            Done = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInElevator = false;
        }
    }
}
