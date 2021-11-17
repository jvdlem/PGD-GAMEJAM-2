using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class locomotionHelper : MonoBehaviour
{
    [SerializeField]private GameObject locomotionMovment;

    public void enableLocomotion()
    {
        locomotionMovment.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
    }
    public void diableLocomotion()
    {
        locomotionMovment.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
    }
}
