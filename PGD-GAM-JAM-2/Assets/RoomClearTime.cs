using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RoomClearTime : MonoBehaviour
{
    float timer;

    bool timerStart;

    public void Update()
    {
        if (timerStart) timer += Time.deltaTime;
    }

    public void StartTimer() 
    {
        timerStart = true;
    }

    public void StopTimer() 
    {
        timerStart = false;
        Analytics.CustomEvent("Time: " + timer);
    }
}
