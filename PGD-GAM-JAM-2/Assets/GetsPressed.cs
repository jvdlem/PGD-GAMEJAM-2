using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsPressed : MonoBehaviour
{
    [SerializeField] GameObject buttonBase;
    [SerializeField] GameObject push;
    float durationOfLerp = 10, lerpTime = 0;
    public void ButtonGetsPressed(RaycastHit hit)
    {
        hit.rigidbody.AddRelativeForce(Vector3.down * 10);
    }
}
