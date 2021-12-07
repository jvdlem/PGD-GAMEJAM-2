using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport2 : MonoBehaviour
{
    private GameObject player;
    public Transform[] Goals;

    private void Start()
    {
        player = this.gameObject;
    }

    public void StartTransport(int GoalID)
    {
        StartCoroutine(TransportPlayer(GoalID));
    }
    public IEnumerator TransportPlayer(int GoalID)
    {

        yield return new WaitForSeconds(5f);

        this.transform.position = Goals[GoalID].position;
        player.GetComponent<PlayerHealthScript>().FadeIn();

    }
}
