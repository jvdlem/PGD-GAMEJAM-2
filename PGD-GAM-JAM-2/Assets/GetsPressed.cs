using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsPressed : MonoBehaviour
{
    [SerializeField] GameObject buttonBase;
    [SerializeField] GameObject originalPushPosition;
    float durationOfLerp = 10, lerpTime = 0;
    public void ButtonGetsPressed(RaycastHit hit)
    {
        hit.transform.position = Vector3.Lerp(originalPushPosition.transform.position, buttonBase.transform.position, lerpTime / durationOfLerp);
    }
}
