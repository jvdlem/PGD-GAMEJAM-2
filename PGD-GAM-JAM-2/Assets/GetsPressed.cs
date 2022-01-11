using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsPressed : MonoBehaviour
{
    [SerializeField] GameObject buttonBase;
    public void ButtonGetsPressed(RaycastHit hit)
    {
        hit.rigidbody.transform.position = buttonBase.transform.position;
    }
}
