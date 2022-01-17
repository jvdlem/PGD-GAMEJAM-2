using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Rigidbody ElevatorButton;

    private FMODUnity.StudioEventEmitter AudioEmitter;

    // Start is called before the first frame update
    void Start()
    {
        ElevatorButton.AddRelativeForce(Vector3.down * 10);
    }
}
