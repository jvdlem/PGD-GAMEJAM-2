using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentStats : MonoBehaviour
{
    public List<float> statList;
    public float spread = 1;
    public float amountOfBullets = 1;
    public float damage = 1;
    public float fullAutoCount = 0;
    public float bulletTime = 1;

    private void Start()
    {
        statList.Add(spread);
        statList.Add(amountOfBullets);
        statList.Add(damage);
        statList.Add(fullAutoCount);
        statList.Add(bulletTime);
    }
}
