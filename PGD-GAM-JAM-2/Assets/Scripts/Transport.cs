using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public GameObject goal;
    public GameObject player;
    public Vector3 worldPos;

    public void StartTransport()
    {
        StartCoroutine(TransportPlayer());
    }
    public IEnumerator TransportPlayer()
    {
        if (goal == null)
        {
            yield return new WaitForSeconds(5f);

            this.transform.position = worldPos;
            player.GetComponent<PlayerHealthScript>().FadeIn();
        }
        else
        {
            yield return new WaitForSeconds(5f);

            this.transform.position = goal.transform.position;
            player.GetComponent<PlayerHealthScript>().FadeIn();
        }
    }
}
