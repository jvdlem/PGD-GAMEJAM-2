using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacmentUi : MonoBehaviour
{
    public GameObject attachmentUILayer;
    private float minDistanceFromAttachment = 5f;
    LayerMask attachments;
    private void Start()
    {
        attachments = LayerMask.GetMask("Interactible");
    }
    private void Update()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        
        if (Physics.Raycast(ray, out hit, minDistanceFromAttachment, attachments))
        {
           
            hit.rigidbody.gameObject.GetComponent<AttachmentStatsUi>().currentState = 1;
            
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * minDistanceFromAttachment, Color.yellow);
    }
}
