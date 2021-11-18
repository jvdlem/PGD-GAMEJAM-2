using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
    [SerializeField] private float spread = 1;
    [SerializeField] private float amountOfBullets = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float fullAutoCount = 0;
    [SerializeField] private float bulletTime = 1;
    [SerializeField] private float bulletSpeed = 30;

    [SerializeField] private GameObject bullet;
    [SerializeField] private XRBaseInteractable aCurrentAddon;

    public List<Attachment> lists;
    public Attachment allStats;
    public Attachment barrelStats;
    public Attachment sightStats;
    public Attachment stockStats;
    public Attachment magazineStats;
    public GameObject holster;

    private bool gatlingSet = false;
    private bool sniperSet = false;
    private bool shotgunSet = false;
    private bool granadeSet = false;

    // Start is called before the first frame update
    void Start()
    {

        lists.Add(allStats);
        lists.Add(barrelStats);
        lists.Add(sightStats);
        lists.Add(stockStats);
        lists.Add(magazineStats);
        foreach (Attachment aList in lists)
        {
            aList.list.Add(spread);
            aList.list.Add(amountOfBullets);
            aList.list.Add(damage);
            aList.list.Add(fullAutoCount);
            aList.list.Add(bulletTime);
            aList.list.Add(bulletSpeed);
        }
    }

    [System.Serializable]
    public class Attachment
    {
       public List<float> list;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) shoot();

        for (int i = 1; i < lists.Count; i++)
        {
            if (this.transform.GetChild(i).GetComponent<SocketCheck>().attached == 0)
            { // on repeat

                aCurrentAddon = this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment;
                lists[i].list = aCurrentAddon.GetComponent<AttachmentStats>().statList;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/Attach", this.transform.position);
                for (int j = 0; j < allStats.list.Count; j++)
                {
                    allStats.list[j] += lists[i].list[j];
                }
                this.transform.GetChild(i).GetComponent<SocketCheck>().attached = 2;
            }
            else if (this.transform.GetChild(i).GetComponent<SocketCheck>().attached == 1)
            {
                for (int j = 0; j < allStats.list.Count; j++)
                {
                    allStats.list[j] -= lists[i].list[j];
                }
                this.transform.GetChild(i).GetComponent<SocketCheck>().attached = 2;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/Attach", this.transform.position);
            }
        }
        if (allStats.list[3] <= 2) { gatlingSet = false; }
        if (gatlingSet)
        {
            for (int i = 0; i < allStats.list[1]; i++)
            {
                bullet.GetComponent<Bullet>().SetStats(allStats.list[2], allStats.list[4], allStats.list[5]);
                Instantiate(bullet, this.transform.position + (transform.forward * 0.5f), this.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
            }
        }
        if (allStats.list[0] <= 0.5) { sniperSet = true; }
        else { sniperSet = false; }
        if (allStats.list[1] >= 5) { shotgunSet = true; }
        else { shotgunSet = false; }
    }

    public void shoot()
    {
        if (allStats.list[3] >= 2) { gatlingSet = !gatlingSet; }
        Debug.Log(gatlingSet);
        if (!gatlingSet)
        {
            for (int i = 0; i < allStats.list[1]; i++)
            {

                bullet.GetComponent<Bullet>().SetStats(allStats.list[2], allStats.list[4], allStats.list[5]);
                Instantiate(bullet, this.transform.position + (transform.forward * 0.5f), this.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Pistol/Shot/Gun 8_1", this.transform.position);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.transform.position = holster.transform.position;
        }
    }
}
