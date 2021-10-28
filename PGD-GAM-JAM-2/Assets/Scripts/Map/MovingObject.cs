using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 realEndPos;
    private Vector3 realStart;
    [SerializeField]private float desiredDuration = 3f;
    private float elapsedTime;
    private float move = 0;
    public float threshold = 1;
    private bool playsound = true;

    private void Start()
    {
        startPosition = this.transform.position;
        realEndPos = startPosition + endPosition;
    }
    private void Update()
    {
        if (move >= threshold)
        {
            if (playsound)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/ConfirmOpen", this.transform.position);
                playsound = false;
            }
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(realStart, realEndPos, percantageComplete);
        }
    }
    public void MoveObject(float add)
    {
        elapsedTime = 0;
        realStart = this.transform.position;
        realEndPos = startPosition + endPosition;
        move += add;
        
    }

    public void MoveBack()
    {
        elapsedTime = 0;
        realStart = this.transform.position;
        realEndPos = startPosition;
    }
}
