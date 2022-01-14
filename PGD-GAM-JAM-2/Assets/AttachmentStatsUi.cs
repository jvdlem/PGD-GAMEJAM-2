using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentStatsUi : MonoBehaviour
{
    [SerializeField] GameObject attachmentStatsUi;
    public int currentState;
    // Start is called before the first frame update
    public void Update()
    {
        
        state();
        currentState = default;
    }
    public void state() 
    {
        switch (currentState) 
        {
            case 1:
                attachmentStatsUi.SetActive(true);
                break;
            default:
                attachmentStatsUi.SetActive(false);
                break;
        }
    }
}
