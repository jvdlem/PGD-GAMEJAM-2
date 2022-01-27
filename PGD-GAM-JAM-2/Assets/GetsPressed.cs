using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsPressed : MonoBehaviour
{
    public void ButtonGetsPressed(RaycastHit hit)
    {
        hit.rigidbody.AddRelativeForce(Vector3.down * 15); //Add force to push button
    }
}
