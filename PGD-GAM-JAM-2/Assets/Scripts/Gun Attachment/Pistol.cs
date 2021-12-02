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

    [SerializeField] private GameObject bullet;
    [SerializeField] private XRBaseInteractable aCurrentAddon;
    [SerializeField] private AudioSource myAudio;
    [SerializeField] private Transform shootPoint;

    public List<Attachment> lists;
    public Attachment allStats;
    public Attachment barrelStats;
    public Attachment sightStats;
    public Attachment stockStats;
    public Attachment magazineStats;

    private bool fullAuto = false;
    public GameObject holster;

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
            }
        }
        if (allStats.list[3] <= 2) { fullAuto = false; }
        if (fullAuto)
        {
            for (int i = 0; i < allStats.list[1]; i++)
            {
                //bullet.GetComponent<Bullet>().Setdmg(damage);
                Instantiate(bullet, this.transform.position + (transform.forward * 0.5f), this.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
            }
        }
    }

    public void shoot()
    {
        if (allStats.list[3] >= 2) { fullAuto = !fullAuto; }
        Debug.Log(fullAuto);

        for (int i = 0; i < allStats.list[1]; i++)
        {
            //bullet.GetComponent<Bullet>().Setdmg(damage);
            myAudio.Play();
            Instantiate(bullet, shootPoint.position + (transform.forward * 0.5f), shootPoint.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
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
