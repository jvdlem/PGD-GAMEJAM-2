using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    private float spread = 1;
    private float amountOfBullets = 1;
    private float damage = 1;
    private float bulletTime = 1;
    private float bulletSpeed = 500;
    private float miniGunSet = 0;
    private float granadeSet = 0;
    private float shotGunSet = 0;
    private float sniperSet = 0;
    public GameObject myMagazine;
    private bool hasAmmo = false;

    [SerializeField] private GameObject currentAmmo;
    [SerializeField] private GameObject bullet;
    [SerializeField] public XRBaseInteractable aCurrentAddon;
    [SerializeField] private GameObject currentShootPoint;
    [SerializeField] private GameObject myshootPoint;
    [SerializeField] StartChoiceControlSystem startControlSystem;
    [SerializeField] ControlManager controlManager;
    [SerializeField] private GameObject MuzzleFlash;

    public List<Attachment> lists = new List<Attachment>();
    public Attachment allStats = new Attachment();
    private Attachment barrelStats = new Attachment();
    private Attachment sightStats = new Attachment();
    private Attachment stockStats = new Attachment();
    private Attachment magazineStats = new Attachment();
    public GameObject holster;
    public GameObject ammoholster;
    public Text myAmmoText;

    private int incommingAttachment = 1;
    private int outGoingAttachment = -1;
    private float startSpread;
    private bool canResize = false;
    public bool canfullAuto = false;
    public bool fullAuto = false;
    public bool isInMenu = false;
    private string pistolShotSound = "event:/Gun/Pistol/Shot/PistolShot";
    private string currentShotSound = "";
    void Start()
    {
        currentShotSound = pistolShotSound;
        gameObject.SetActive(true);
        lists.Add(allStats);
        lists.Add(barrelStats);
        lists.Add(magazineStats);
        lists.Add(sightStats);
        lists.Add(stockStats);

        foreach (Attachment aList in lists)
        {
            if (aList != null)
            {
                aList.list.Add(spread);
                aList.list.Add(amountOfBullets);
                aList.list.Add(damage);
                aList.list.Add(bulletTime);
                aList.list.Add(bulletSpeed);
                aList.list.Add(miniGunSet);
                aList.list.Add(granadeSet);
                aList.list.Add(shotGunSet);
                aList.list.Add(sniperSet);
            }
        }


    }

    [System.Serializable]
    public class Attachment
    {
        public List<float> list = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allStats.list[0] < 0) allStats.list[0] = 0;

        MuzzleFlash.transform.position = currentShootPoint.transform.position;
        MuzzleFlash.transform.rotation = currentShootPoint.transform.rotation;
        if (isInMenu == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (startControlSystem != null)
                {
                    shoot();
                }
                else if (controlManager.Keyboard)
                {
                    shoot();
                }

            }
            if (Input.GetButtonUp("Fire1"))
            {
                fullAuto = false;
            }
            if (Input.GetKeyDown("r"))
            {
                StartCoroutine(Reload());
                
            }
        }
        for (int i = 1; i < lists.Count; i++)
        {

            if (this.transform.GetChild(i).GetComponent<SocketCheck>().attached == 0)
            {
                aCurrentAddon = this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment;

                lists[i].list = aCurrentAddon.GetComponent<AttachmentStats>().statList;
                for (int j = 0; j < allStats.list.Count; j++)
                {
                    allStats.list[j] += lists[i].list[j];
                }

                if (i == 1 && aCurrentAddon.GetComponent<ShootPointScanner>().GetShootPoint() != null)
                {
                    currentShootPoint = aCurrentAddon.GetComponent<ShootPointScanner>().GetShootPoint();
                    currentShotSound = aCurrentAddon.GetComponent<ShootPointScanner>().GetShootSound();
                    MuzzleFlash.transform.localScale = new Vector3(aCurrentAddon.GetComponent<ShootPointScanner>().GetShootScale(), aCurrentAddon.GetComponent<ShootPointScanner>().GetShootScale(), aCurrentAddon.GetComponent<ShootPointScanner>().GetShootScale());
                }
                if (i == 2 && aCurrentAddon.GetComponent<AmmoType>().GetAmmoType() != null)
                {
                    currentAmmo = aCurrentAddon.GetComponent<AmmoType>().GetAmmoType();
                    myMagazine = aCurrentAddon.GetComponent<AmmoType>().GetMyObject();
                    if (ammoholster != null)
                    {
                        ammoholster.GetComponent<AmmoSocket>().SetAmmo(myMagazine);
                    }
                    myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
                    hasAmmo = true;

                }
                CheckSet(1, this.transform.GetChild(i).GetComponent<SocketCheck>());
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/Attach", this.gameObject.transform.position);
                
                this.transform.GetChild(i).GetComponent<SocketCheck>().attached = 2;
                currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
            }
            else if (this.transform.GetChild(i).GetComponent<SocketCheck>().attached == 1)
            {
                for (int j = 0; j < allStats.list.Count; j++)
                {
                    allStats.list[j] -= lists[i].list[j];
                }


                if (i == 1)
                {
                    currentShotSound = pistolShotSound;
                    currentShootPoint = myshootPoint;
                    MuzzleFlash.transform.position = currentShootPoint.transform.position;
                    MuzzleFlash.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                if (i == 2)
                {
                    currentAmmo.GetComponent<Projectille>().Reset();
                    currentAmmo = bullet;
                    myAmmoText.text = "Infinite";
                }
                CheckSet(-1, this.transform.GetChild(i).GetComponent<SocketCheck>());
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/Dettach", this.gameObject.transform.position);

                this.transform.GetChild(i).GetComponent<SocketCheck>().attached = 2;
                currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
            }

        }
        if (fullAuto)
        {
            StartCoroutine(CanFullAuto());
        }
        if (fullAuto == false)
        {
            StopCoroutine(CanFullAuto());
        }
       
    }
    IEnumerator Reload()
    {
        if (myMagazine.GetComponent<AmmoType>() != null)
        {
            yield return new WaitForSeconds(1);
            myMagazine.GetComponent<AmmoType>().AmmoAmount = myMagazine.GetComponent<AmmoType>().maxAmmo;
            myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
            StopCoroutine(Reload());
        }
    }
        public void stopFullAuto()
    {
        fullAuto = false;
        allStats.list[0] = startSpread;
    }
    IEnumerator CanFullAuto()
    {
        if (myMagazine.GetComponent<AmmoType>().AmmoAmount >= 1)
        {
            FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
            Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
            MuzzleFlash.GetComponent<VisualEffect>().Play();
            if (canResize) { allStats.list[0] *= 0.9f; }
            myMagazine.GetComponent<AmmoType>().removeAmmoAmount(1);
            myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
            yield return new WaitForSeconds(0.1f);
        }

    }
    public void shoot()
    {
        if (canfullAuto)
        {
            fullAuto = true;
            startSpread = allStats.list[0];

        }
        if (hasAmmo == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
            Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
            MuzzleFlash.GetComponent<VisualEffect>().Play();
        }
        if (hasAmmo && myMagazine.GetComponent<AmmoType>().AmmoAmount >= 1)
        {
            for (int i = 0; i < allStats.list[1]; i++)
            {
                FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
                Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), Random.Range(-allStats.list[0], allStats.list[0]) * (Mathf.PI / 180), 1));
                MuzzleFlash.GetComponent<VisualEffect>().Play();
            }
            myMagazine.GetComponent<AmmoType>().removeAmmoAmount(1);
            myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Out of Ammo", this.gameObject.transform.position);
        }
                
    }

    private void CheckSet(int scale, SocketCheck aSocket)
    {
        for (int i = 5; i < allStats.list.Count; i++)
        {

            if (allStats.list[i] >= 2)
            {
                switch (i - 5)
                {
                    case 0:
                        {
                            if (allStats.list[i] >= 4 && scale >= incommingAttachment && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale <= outGoingAttachment && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                canResize = scale >= 1;
                                SuperPowerAttachment(scale);
                            }
                            if (allStats.list[i] == 2 && scale <= -1 && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale >= 1 && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                canfullAuto = scale >= 1;
                                NormalPowerAttachment(scale);
                            }
                        }
                        break;
                    case 1:
                        {
                            if (allStats.list[i] >= 4 && scale >= incommingAttachment && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale <= outGoingAttachment && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                if (currentAmmo.GetComponent<Granade>() != null)
                                {
                                    currentAmmo.GetComponent<Granade>().UpdateScale(10f * scale, true, scale);
                                    SuperPowerAttachment(scale);
                                }

                            }
                            if (allStats.list[i] == 2 && scale <= -1 && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale >= 1 && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                if (currentAmmo.GetComponent<Granade>() != null)
                                {
                                    currentAmmo.GetComponent<Granade>().UpdateScale(1, scale >= 1, scale);
                                }
                                NormalPowerAttachment(scale);
                            }
                        }
                        break;
                    case 2:
                        {
                            if (allStats.list[i] >= 4 && scale >= incommingAttachment && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale <= outGoingAttachment && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                allStats.list[1] += 10 * scale;
                                SuperPowerAttachment(scale);
                            }
                            if (allStats.list[i] == 2 && scale <= -1 && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale >= 1 && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                allStats.list[1] += 5 * scale;
                                NormalPowerAttachment(scale);
                            }
                        }
                        break;
                    case 3:
                        {
                            if (allStats.list[i] >= 4 && scale >= incommingAttachment && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale <= outGoingAttachment && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                
                                SuperPowerAttachment(scale);
                                if (scale >= 1)
                                {
                                    currentAmmo.transform.GetChild(0).GetComponent<MeshCollider>().isTrigger = scale >= 1;
                                    currentShotSound = "event:/Gun/Sniper/Shot/Sniper_Set";
                                }
                                if (scale <= -1)
                                {
                                    currentAmmo.transform.GetChild(0).GetComponent<MeshCollider>().isTrigger = scale >= 1;
                                    currentShotSound = "event:/Gun/Sniper/Shot/Sniper_Barrel";
                                }
                            }
                            if (allStats.list[i] == 2 && scale <= -1 && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale >= 1 && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                allStats.list[4] += 10000 * scale;
                                NormalPowerAttachment(scale);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void SuperPowerAttachment(int scale)
    {
        for (int i = 1; i < lists.Count; i++)
        {
            if (this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment != null)
            {
                this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment.GetComponent<AttachmentPowerUP>().SuperPowered(scale);
            }
        }

    }
    private void NormalPowerAttachment(int scale)
    {
        for (int i = 1; i < lists.Count; i++)
        {
            if (this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment != null)
            {
                this.transform.GetChild(i).GetComponent<SocketCheck>().Attatchment.GetComponent<AttachmentPowerUP>().NormalPowered(scale);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (holster != null)
            {
                this.transform.position = holster.transform.position;
            }
        }
    }

    public void ToggleVRPistol()
    {
        gameObject.SetActive(false);
    }
}
