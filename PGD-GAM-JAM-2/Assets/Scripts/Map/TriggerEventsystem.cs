using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventsystem : MonoBehaviour
{
    public UnityEvent onTriggerEnter, onTriggerExit;
    public MusicLoop musicLoop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Triggerd();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Deactivate();
        }
    }
    private void Triggerd()
    {
        onTriggerEnter.Invoke();
    }

    private void Deactivate()
    {
        onTriggerExit.Invoke();
    }
}
