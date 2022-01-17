using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentStats : MonoBehaviour
{
    public List<float> statList;
    public float spread = 1;
    public float amountOfBullets = 1;
    public float damage = 1;
    public float bulletTime = 1;
    public float bulletSpeed = 1;
    public float miniGunSet = 0;
    public float grenadeSet = 0;
    public float shotGunSet = 0;
    public float sniperSet = 0;



    private void Start()
    {
        statList.Add(spread);
        statList.Add(amountOfBullets);
        statList.Add(damage);
        statList.Add(bulletTime);
        statList.Add(bulletSpeed);
        statList.Add(miniGunSet);
        statList.Add(grenadeSet);
        statList.Add(shotGunSet);
        statList.Add(sniperSet);
    }
}
