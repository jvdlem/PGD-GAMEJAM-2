using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    private float triggerTarget;
    private float gripTarget;
    private float gripCurrent;
    private float triggerCurrent;
    [SerializeField] float speed;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        AnimateHand();
    }

    internal void Setgrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }


    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }
}
