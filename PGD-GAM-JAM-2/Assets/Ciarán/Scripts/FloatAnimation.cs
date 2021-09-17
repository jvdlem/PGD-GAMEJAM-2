using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    float floatSpan = 2.0f;
    float speed = 1.0f;
    float bobbingVariable = 6.0f;

    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 bobbingEffect = new Vector3(0, (float)(startY + Mathf.Sin(Time.time * speed) * floatSpan / bobbingVariable), 0);
        
        this.transform.position = bobbingEffect;
       
    } 
}
