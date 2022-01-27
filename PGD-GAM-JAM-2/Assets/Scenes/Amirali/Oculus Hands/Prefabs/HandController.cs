using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand hand;

    void Start()
    {
        
        controller = GetComponent<ActionBasedController>();
    }

  
    void Update()
    {
        //if we have a hand model with script check the value of trigger and grip and set the values for our other script
        if (hand == null) { hand = GetComponentInChildren<Hand>(); }
        hand.Setgrip(controller.activateAction.action.ReadValue<float>());
        hand.SetTrigger(controller.selectAction.action.ReadValue<float>());

    }
}
