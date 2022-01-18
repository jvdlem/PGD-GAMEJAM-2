using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSpawner1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> Attachments = new List<GameObject>();
    [SerializeField]private GameObject spawnPoint;
    public void SpawnAttachment()
    {
        StartCoroutine(WaitForSpawn(2));
    }

    IEnumerator WaitForSpawn(int seconds)
    {

        yield return new WaitForSeconds(seconds);
        int i = Random.Range(0, Attachments.Count);
        GameObject anAttachment = Instantiate(Attachments[i], this.spawnPoint.transform.position, Quaternion.identity);
        StopCoroutine(WaitForSpawn(0));
        
    }
}
