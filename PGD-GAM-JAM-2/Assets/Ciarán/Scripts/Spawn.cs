using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool roomIsCleared;
    public GameObject Attachement1, Attachement2, Attachement3;
    public GameObject[] randomAttachemt = new GameObject[3];
    public int randomIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        randomAttachemt[1] = Attachement1;
        randomAttachemt[2] = Attachement2;
        randomAttachemt[3] = Attachement3;
        randomIndex = Random.Range(0, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomIsCleared)
        {
            randomAttachemt[randomIndex].transform.position = transform.position;
        }
    }
}
