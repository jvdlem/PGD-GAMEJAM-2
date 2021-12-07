using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public GameObject goal;
    public GameObject player;

    public void StartTransport() 
    {
        StartCoroutine(TransportPlayer());
    }
    public IEnumerator TransportPlayer() 
    {

        yield return new WaitForSeconds(5f);

        this.transform.position = goal.transform.position;
        player.GetComponent<PlayerHealthScript>().FadeIn();
    }
}
