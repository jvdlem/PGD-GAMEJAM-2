using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitIndication : MonoBehaviour
{
    public GameObject subIndication;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthScript.indicatorOnOrOff)
        {
            subIndication.SetActive(true);
        }
        else if (!PlayerHealthScript.indicatorOnOrOff)
        {
            subIndication.SetActive(false);
        }
    }
}
