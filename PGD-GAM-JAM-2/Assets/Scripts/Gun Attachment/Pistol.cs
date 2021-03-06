using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    [SerializeField] ExitMenuScript exitmenuscript;

    [SerializeField] private float spread = 100;
    [SerializeField] private float amountOfBullets = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float bulletTime = 1;
    [SerializeField] private float bulletSpeed = 1000;
    [SerializeField] private float miniGunSet = 0;
    [SerializeField] private float granadeSet = 0;
    [SerializeField] private float shotGunSet = 0;
    [SerializeField] private float sniperSet = 0;
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
    [SerializeField] private crosshairScript Croshare;
    public GameObject crosshair;
    private bool doneLooking;

    public List<Attachment> lists = new List<Attachment>();
    public Attachment allStats = new Attachment();
    private Attachment barrelStats = new Attachment();
    private Attachment sightStats = new Attachment();
    private Attachment stockStats = new Attachment();
    private Attachment magazineStats = new Attachment();
    public GameObject holster;
    public GameObject ammoholster;
    public Text myAmmoText;
    public Transform Gun;
    public Transform cam;

    private int incommingAttachment = 1;
    private int outGoingAttachment = -1;
    private float startSpread;
    private bool canResize = false;
    public bool canfullAuto = false;
    public bool fullAuto = false;
    public bool isInMenu = false;
    public static bool reloading;
    private string pistolShotSound = "event:/Gun/Pistol/Shot/PistolShot";
    private string currentShotSound = "";

    void Start()
    {
        startSpread = spread;
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
        if (currentAmmo != null)
        {
            currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
        }

    }

    [System.Serializable]
    public class Attachment
    {
        public List<float> list = new List<float>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MuzzleFlash.transform.position = currentShootPoint.transform.position;
        MuzzleFlash.transform.rotation = currentShootPoint.transform.rotation;
    }


    void Update()
    {
        if ((startControlSystem != null && startControlSystem.Keyboard) || (controlManager != null && controlManager.Keyboard))
        {
            RotateGun();
        }

        if (ControlManager.doneChoosing && !doneLooking)
        {
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            if (crosshair != null) Croshare = crosshair.GetComponent<crosshairScript>();
            doneLooking = true;
        }

        if (allStats.list[0] <= 0)
        {
            allStats.list[0] = 0;
        }

        if (exitmenuscript.menuOn == false)
        {

            if (isInMenu == false)
            {
                if (Input.GetButtonDown("Fire1") && !reloading && playerAimScript.reloadTimer >= 1)
                {
                    if (startControlSystem != null && startControlSystem.Keyboard)
                    {
                        shoot();
                    }
                    else if (controlManager != null && controlManager.Keyboard)
                    {
                        shoot();
                    }
                }

                if (Input.GetKeyDown("r") && !reloading && hasAmmo)
                {
                    StartCoroutine(Reload());
                    reloading = true;
                    fullAuto = false;
                    stopFullAuto();
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    fullAuto = false;
                    stopFullAuto();
                }
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
                if (i == 3 && aCurrentAddon.transform.GetChild(1).GetComponent<ScopeCamera>() != null)
                {
                    aCurrentAddon.transform.GetChild(1).gameObject.SetActive(true);
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
                    hasAmmo = false;
                    myAmmoText.text = "Infinite";
                    currentAmmo.GetComponent<Projectille>().Reset();
                    currentAmmo = bullet;

                }
                if (i == 3 && aCurrentAddon.transform.GetChild(1).GetComponent<ScopeCamera>() != null)
                {
                    aCurrentAddon.transform.GetChild(1).gameObject.SetActive(false);
                }
                CheckSet(-1, this.transform.GetChild(i).GetComponent<SocketCheck>());
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/Dettach", this.gameObject.transform.position);

                this.transform.GetChild(i).GetComponent<SocketCheck>().attached = 2;
                currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
            }

        }

        if (canfullAuto == false)
        {
            fullAuto = false;
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
    void RotateGun()
    {
        if ((startControlSystem != null && !startControlSystem.VR) || (controlManager != null && !controlManager.VR))
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo) && !playerAimScript.isAiming)
            {
                Vector3 direction = hitInfo.point - Gun.position;
                Gun.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                Gun.rotation = this.transform.rotation;
            }
        }
    }
    IEnumerator Reload()
    {
        if (myMagazine != null && myMagazine.GetComponent<AmmoType>() != null)
        {
            yield return new WaitForSeconds(1);
            myMagazine.GetComponent<AmmoType>().AmmoAmount = myMagazine.GetComponent<AmmoType>().maxAmmo;
            myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
            reloading = false;
            StopCoroutine(Reload());
        }
    }
    public void stopFullAuto()
    {
        fullAuto = false;
        //allStats.list[0] = startSpread;
    }
    IEnumerator CanFullAuto()
    {
        if (myMagazine != null && myMagazine.GetComponent<AmmoType>().AmmoAmount >= 1 || myMagazine == null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
            Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(Random.Range(-allStats.list[0], allStats.list[0]), Random.Range(-allStats.list[0], allStats.list[0]), 1));
            MuzzleFlash.GetComponent<VisualEffect>().Play();
            MuzzleFlash.transform.position = currentShootPoint.transform.position;
            MuzzleFlash.transform.rotation = currentShootPoint.transform.rotation;
            if (canResize) { allStats.list[0] *= 0.99f; }
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
            currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
            for (int i = 0; i < allStats.list[1]; i++)
            {
                float randomX = Random.Range(-allStats.list[0], allStats.list[0]);
                float randomY = Random.Range(-allStats.list[0], allStats.list[0]);
                FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
                Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(randomX, randomY, randomX));
                MuzzleFlash.GetComponent<VisualEffect>().Play();
            }
            MuzzleFlash.transform.position = currentShootPoint.transform.position;
            MuzzleFlash.transform.rotation = currentShootPoint.transform.rotation;

        }
        if (myMagazine != null && hasAmmo && myMagazine.GetComponent<AmmoType>().AmmoAmount >= 1)
        {
            currentAmmo.GetComponent<Projectille>().Stats(allStats.list[4], allStats.list[3], allStats.list[2]);
            for (int i = 0; i < allStats.list[1]; i++)
            {

                float randomX = Random.Range(-allStats.list[0], allStats.list[0]);
                float randomY = Random.Range(-allStats.list[0], allStats.list[0]);
                FMODUnity.RuntimeManager.PlayOneShot(currentShotSound, this.gameObject.transform.position);
                Instantiate(currentAmmo, currentShootPoint.transform.position + (transform.forward * 0.5f), currentShootPoint.transform.rotation * Quaternion.Euler(randomX, randomY, randomX));
                MuzzleFlash.GetComponent<VisualEffect>().Play();

            }
            myMagazine.GetComponent<AmmoType>().removeAmmoAmount(1);
            myAmmoText.text = myMagazine.GetComponent<AmmoType>().GetAmmoAmount().ToString();
            MuzzleFlash.transform.position = currentShootPoint.transform.position;
            MuzzleFlash.transform.rotation = currentShootPoint.transform.rotation;
        }
        else if (myMagazine != null && myMagazine.GetComponent<AmmoType>().AmmoAmount <= 0)
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
                                    currentAmmo.GetComponent<Granade>().UpdateScale(2f * scale, true, scale);
                                    SuperPowerAttachment(scale);
                                }
                                else
                                {
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
                                allStats.list[0] += 10 * scale;
                                SuperPowerAttachment(scale);
                            }
                            if (allStats.list[i] == 2 && scale <= -1 && aSocket.exitAttachment.GetComponent<AttachmentStats>().statList[i] >= 1 || allStats.list[i] == 3 && scale >= 1 && aCurrentAddon.GetComponent<AttachmentStats>().statList[i] >= 1)
                            {
                                allStats.list[1] += 5 * scale;
                                allStats.list[0] += 10 * scale;
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
                                    currentAmmo.transform.GetComponent<BoxCollider>().enabled = true;
                                    currentAmmo.transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
                                    currentShotSound = "event:/Gun/Sniper/Shot/Sniper_Set";
                                }
                                if (scale <= -1)
                                {
                                    currentAmmo.transform.GetComponent<BoxCollider>().enabled = false;
                                    currentAmmo.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;
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
