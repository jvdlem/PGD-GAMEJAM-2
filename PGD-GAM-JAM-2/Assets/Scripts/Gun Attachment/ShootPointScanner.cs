using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointScanner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject shootpoint;
    [SerializeField] private float scale = 0.1f;
    [SerializeField] private string myShootSound = "";
    public GameObject GetShootPoint()
    {
        return shootpoint;
    }
    public float GetShootScale()
    {
        return scale;
    }

    public string GetShootSound()
    {
        return myShootSound;
    }
}
