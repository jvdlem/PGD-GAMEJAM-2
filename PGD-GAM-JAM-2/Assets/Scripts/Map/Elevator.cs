using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Rigidbody ElevatorButton;

    public FMODUnity.StudioEventEmitter AudioEmitter;

    public bool PlayerInElevator;

    // Start is called before the first frame update
    void Start()
    {
        ElevatorButton.AddRelativeForce(Vector3.down * 10);
        AudioEmitter.Play();
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
