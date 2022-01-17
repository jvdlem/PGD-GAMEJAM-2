using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    private static int amountOfEyes = 3;
    private EyeBossScript[] Eyes = new EyeBossScript[amountOfEyes];
    public GameObject Player;

    private int bossMaxHealth = 1000;
    public int BossHealth;
    public Slider healthSlider;
    public Transform MinionSpawnLocation;
    public GameObject Minions;
    private int minionAmount = 5;
    private bool minionsSpawned;
    private int previousEye;

    private float EyeLookSpeedDefault = 3;
    private float EyeLookSpeedTracking = 10;

    private float laserChargeTime = 3;
    private float laserChargeTimer;
    public GameObject Laser;

    private float eyeShootDelay = 1;
    private float eyeShootTimer;
    public GameObject Bullets;

    public int NextHealthTrigger;
    private int AmountOfCycles = 10;

    public bool CycleToNextEye = false;

    private float BossDiesFadeTime = 3;
    private float BossDiesFadeTimer;

    public int bossState;

    private float bossIsWaitingTime = 5;
    private float bossIsWaitingTimer;

    private FMODUnity.StudioEventEmitter AudioEmitter;
    public FMODUnity.EventReference[] SoundEffects;

    void Start()
    {
        bossState = 0;

        for (int i = 0; i < amountOfEyes; i++)
        {
            Eyes[i] = gameObject.transform.GetChild(i).GetComponent<EyeBossScript>();
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        healthSlider.maxValue = bossMaxHealth;
    }

    void Update()
    {
        healthSlider.value = BossHealth;
        healthSlider.transform.LookAt(Player.transform);

        switch (bossState)
        {
            //boss spawnes/resets state
            case 0:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(true);
                    eye.renderer.material.color = Color.white;
                }
                BossHealth = bossMaxHealth;
                CycleToNextEye = false;

                AudioEmitter.EventReference = SoundEffects[0];
                AudioEmitter.Play();
                bossState = 1;

                break;

            //boss is waiting state
            case 1:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.EyeIsActive = false;
                }

                //the next health trigger is calculated (1000 - (1000 / 10))
                NextHealthTrigger = BossHealth - (bossMaxHealth / AmountOfCycles);

                if (bossIsWaitingTimer < bossIsWaitingTime)
                {
                    bossIsWaitingTimer += Time.deltaTime;
                }
                else
                {
                    bossState = Random.Range(2, 5);
                    while (bossState == previousEye)
                    {
                        bossState = Random.Range(2, 5);
                    }
                    bossIsWaitingTimer = 0;
                }
                break;

            //boss uses left eye state
            case 2:
                Eyes[0].EyeIsActive = true;
                Eyes[0].lookSpeed = EyeLookSpeedTracking;
                previousEye = bossState;

                if (eyeShootTimer < eyeShootDelay)
                {
                    eyeShootTimer += Time.deltaTime;
                }
                else
                {
                    Instantiate(Bullets, Eyes[0].transform.position, Eyes[0].transform.rotation, transform);
                    eyeShootDelay = Random.Range(0.1f, 0.8f); //change shoot delay so that the shots are eratic instead of linear intervals

                    AudioEmitter.EventReference = SoundEffects[1];
                    AudioEmitter.Play();

                    eyeShootTimer = 0;
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    Eyes[0].lookSpeed = EyeLookSpeedDefault;
                    bossState = 1;
                }
                break;

            //boss uses middle eye state
            case 3:
                Eyes[1].EyeIsActive = true;
                previousEye = bossState;

                if (Minions != null && minionsSpawned == false)
                {
                    for (int i = 0; i < minionAmount; i++)
                    {
                        Instantiate(Minions, MinionSpawnLocation.position, MinionSpawnLocation.rotation, MinionSpawnLocation);
                    }
                    minionsSpawned = true;

                    AudioEmitter.EventReference = SoundEffects[2];
                    AudioEmitter.Play();
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    minionsSpawned = false;
                    bossState = 1;
                }
                break;

            //boss uses right eye state
            case 4:
                Eyes[2].EyeIsActive = true;
                Eyes[2].lookSpeed = EyeLookSpeedTracking;
                previousEye = bossState;

                if (laserChargeTimer < laserChargeTime)
                {
                    laserChargeTimer += Time.deltaTime;
                    Eyes[2].renderer.material.color = Color.Lerp(Color.white, Color.red, laserChargeTimer / 3);
                }
                else
                {
                    if (Eyes[2].transform.childCount == 0)
                    {
                        Instantiate(Laser, Eyes[2].transform.position, Quaternion.Euler(Eyes[2].transform.rotation.eulerAngles.x - 90, Eyes[2].transform.rotation.eulerAngles.y, Eyes[2].transform.rotation.eulerAngles.z), Eyes[2].transform);

                        AudioEmitter.EventReference = SoundEffects[3];
                        AudioEmitter.Play();
                    }
                    Eyes[2].renderer.material.color = Color.red;
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    if(Eyes[2].transform.childCount != 0)
                    {
                        Destroy(Eyes[2].transform.GetChild(0).gameObject);
                        AudioEmitter.Stop();
                    }
                    Eyes[2].lookSpeed = EyeLookSpeedDefault;
                    laserChargeTimer = 0;
                    bossState = 1;
                }
                break;

            //boss dies state;
            case 5:

                AudioEmitter.EventReference = SoundEffects[4];
                AudioEmitter.Play();

                if (Eyes[2].transform.childCount != 0)
                {
                    Destroy(Eyes[2].transform.GetChild(0).gameObject);
                }

                if (BossDiesFadeTimer < BossDiesFadeTime)
                {
                    BossDiesFadeTimer += Time.deltaTime;

                    foreach (EyeBossScript eyes in Eyes)
                    {
                        eyes.renderer.material.color = Color.white;

                        eyes.renderer.material.color = Color.Lerp(eyes.renderer.material.color, new Color(
                            eyes.renderer.material.color.r,
                            eyes.renderer.material.color.g,
                            eyes.renderer.material.color.b,
                            0),
                            BossDiesFadeTimer/ 2);
                    }
                }
                else
                {
                    bossState = 6;
                }

                break;

            //boss goes inactive state
            case 6:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(false);
                }

                BossDiesFadeTimer = 0;

                bossState = 7;
                break;

            //boss is inactive state
            case 7:
                break;
        }
    }
}
