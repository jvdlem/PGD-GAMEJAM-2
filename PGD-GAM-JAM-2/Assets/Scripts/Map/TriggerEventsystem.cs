using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventsystem : MonoBehaviour
{
    public UnityEvent onTriggerEnter, onTriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Tiggerd();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Deactivate();
        }
    }
    private void Tiggerd()
    {
        //myAudio.Play();
        onTriggerEnter.Invoke();

    }

    private void Deactivate()
    {
        onTriggerExit.Invoke();
    }
}
