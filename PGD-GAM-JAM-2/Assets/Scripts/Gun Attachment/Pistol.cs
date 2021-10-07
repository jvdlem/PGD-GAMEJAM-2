using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private XRBaseInteractable CurrentBarrelAddon;
    [SerializeField] private XRBaseInteractable PrevBarrelAddon;
    [SerializeField] private XRBaseInteractable SightAddon;
    [SerializeField] private XRBaseInteractable StockAddon;
    [SerializeField] private XRBaseInteractable MagazineAddon;


    public List<float> allStats;
    public List<float> barrelStats;
    public List<float> sightStats;
    public List<float> stockStats;
    public List<float> magazineStats;

    private float spread = 1;
    private float amountOfBullets = 1;
    private float damage = 1;
    private float fullAutoCount = 0;
    private float bulletTime = 1;

    private bool fullAuto = false;

    private bool BAS = true;
    private bool SAS;
    private bool STAS;
    private bool MAS;
    private bool BRS = false;
    private bool SRS;
    private bool STRS;
    private bool MRS;
    // Start is called before the first frame update
    void Start()
    {
        allStats.Add(spread);
        allStats.Add(amountOfBullets);
        allStats.Add(damage);
        allStats.Add(fullAutoCount);
        allStats.Add(bulletTime);

        barrelStats.Add(spread);
        barrelStats.Add(amountOfBullets);
        barrelStats.Add(damage);
        barrelStats.Add(fullAutoCount);
        barrelStats.Add(bulletTime);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (this.transform.GetChild(1).GetComponent<SocketCheck>().Attatchment != null)
    //    { // on repeat

    //        CurrentBarrelAddon = this.transform.GetChild(1).GetComponent<SocketCheck>().Attatchment;
    //        if (PrevBarrelAddon == CurrentBarrelAddon)
    //        {
    //            if (BAS)
    //            {
    //                BRS = true;

    //                for (int i = 0; i < allStats.Count; i++)
    //                {
    //                    barrelStats[i] = PrevBarrelAddon.GetComponent<AttachmentStats>().statList[i];
    //                    allStats[i] += barrelStats[i];
    //                }
    //                BAS = false;
    //            }
    //            PrevBarrelAddon = CurrentBarrelAddon;

    //        }
    //        else
    //        {
    //            PrevBarrelAddon = CurrentBarrelAddon;
    //            if (BRS)
    //            {
    //                for (int i = 0; i < allStats.Count; i++)
    //                {
    //                    allStats[i] -= barrelStats[i];
    //                }
    //                BRS = false;
    //                BAS = true;

    //            }
    //        }
    //    }



    //    if (this.transform.GetChild(2).GetComponent<SocketCheck>().Attatchment != null)
    //    {

    //        SightAddon = this.transform.GetChild(2).GetComponent<SocketCheck>().Attatchment;
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            sightStats[i] = SightAddon.GetComponent<AttachmentStats>().statList[i];
    //            allStats[i] += sightStats[i];
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            allStats[i] -= sightStats[i];
    //        }
    //    }

    //    if (this.transform.GetChild(3).GetComponent<SocketCheck>().Attatchment != null)
    //    {
    //        StockAddon = this.transform.GetChild(3).GetComponent<SocketCheck>().Attatchment;
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            stockStats[i] = StockAddon.GetComponent<AttachmentStats>().statList[i];
    //            allStats[i] += stockStats[i];
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            allStats[i] -= stockStats[i];
    //        }
    //    }

    //    if (this.transform.GetChild(4).GetComponent<SocketCheck>().Attatchment != null)
    //    {
    //        MagazineAddon = this.transform.GetChild(4).GetComponent<SocketCheck>().Attatchment;
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            magazineStats[i] = MagazineAddon.GetComponent<AttachmentStats>().statList[i];
    //            allStats[i] += magazineStats[i];
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < allStats.Count; i++)
    //        {
    //            allStats[i] -= magazineStats[i];
    //        }
    //    }
    //    if (fullAuto)
    //    {
    //        bullet.GetComponent<Bullet>().Setdmg(damage);
    //        Instantiate(bullet, this.transform.position + (transform.forward * 0.5f), this.transform.rotation * Quaternion.Euler(Random.Range(-spread, spread) * (Mathf.PI / 180), Random.Range(-spread, spread) * (Mathf.PI / 180), 1));
    //    }

    //    this.transform.GetChild(1).GetComponent<SocketCheck>().Attatchment = null;
    //}

    public void shoot()
    {
        
        if (fullAutoCount >= 2) { fullAuto = !fullAuto; }
        else { fullAuto = false; }
        for (int i = 0; i < allStats[1]; i++)
        {
            //bullet.GetComponent<Bullet>().Setdmg(damage);
            Instantiate(bullet, this.transform.position + (transform.forward * 0.5f), this.transform.rotation * Quaternion.Euler(Random.Range(-allStats[0], allStats[0]) * (Mathf.PI / 180), Random.Range(-allStats[0], allStats[0]) * (Mathf.PI / 180), 1));
        }
    }
}
