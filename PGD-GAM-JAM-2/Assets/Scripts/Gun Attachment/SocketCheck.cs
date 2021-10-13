using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketCheck : XRSocketInteractor
{
    public string targetTag = string.Empty;
    public XRBaseInteractable Attatchment;
    public int attached = 2;

    private void Update()
    {
        //Debug.Log(hasObject);
    }
    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        attached = 1;
        Debug.Log(interactable);
        base.OnSelectExited(interactable);
        Attatchment = null;
    }

    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {
        attached = 0;
        Attatchment = interactable;
        base.OnSelectEntering(interactable);
    }

}
