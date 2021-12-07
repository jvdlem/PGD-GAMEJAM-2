using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorpicker : MonoBehaviour
{
    Color startColor;
    public Color AttachmentColor;
    bool canSwitch = true;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 0)
        {
            GameObject child = transform.GetChild(0).gameObject;
            startColor = child.GetComponent<Renderer>().material.color;
        }
    }

    public void switchColor()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                
                if (canSwitch)
                {
                    child.GetComponentInChildren<Renderer>().material.color = AttachmentColor;
                }
                else
                {
                    child.GetComponentInChildren<Renderer>().material.color = startColor;
                }
            }
        }

        canSwitch = !canSwitch;
    }
}
