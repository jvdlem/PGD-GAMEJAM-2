using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Rigidbody ElevatorButton;

    public FMODUnity.StudioEventEmitter AudioEmitter;

    public bool PlayerInElevator, Done, OpenElevatorDoors;

    float elevatorTimer = 2;

    public GameObject EndText;

    private void Update()
    {
        if (OpenElevatorDoors)
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
    }

    public void OpenElevator()
    {
        OpenElevatorDoors = true;
    }

    public void ShowEndText()
    {
        EndText.SetActive(true);
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
