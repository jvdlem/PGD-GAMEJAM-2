using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLights : MonoBehaviour
{
    [SerializeField] private GameObject lightParent;
    public void LightsBattelForm()
    {
        for(int i = 0; i< lightParent.transform.childCount; i++)
        {
            lightParent.transform.GetChild(i).GetComponent<CrystalChange>().BattelForm();
        }
    }

    public void LightsNormalForm()
    {
        for (int i = 0; i < lightParent.transform.childCount; i++)
        {
            lightParent.transform.GetChild(i).GetComponent<CrystalChange>().normalForm();
        }
    }
}
