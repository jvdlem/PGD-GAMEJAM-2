using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{
    [SerializeField]public GameObject player;
    void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y,0);
    }
}
