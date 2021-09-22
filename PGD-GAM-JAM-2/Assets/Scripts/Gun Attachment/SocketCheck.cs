using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketCheck : XRSocketInteractor
{
    public string targetTag = string.Empty;
    public XRBaseInteractable Attatchment;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        Attatchment = interactable;
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }
}
