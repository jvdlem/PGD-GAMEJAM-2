using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : EnemyBaseScript
{
    public enum BossStates
    {
        ResetState,
        WaitingState,
        LeftEyeState,
        MiddleEyeState,
        RightEyeState,
        DieState,
        PrepareForIdleState,
        IdleState
    }
    public BossStates CurrentBossState;

    private static int amountOfEyes = 3;
    private EyeBossScript[] Eyes = new EyeBossScript[amountOfEyes];
    public GameObject bulletspawn;

    private int bossMaxHealth = 1000;
    public int BossHealth;
    public Slider healthSlider;
    public Transform MinionSpawnLocation;
    public GameObject Minions;
    private int minionAmount = 5;
    private bool minionsSpawned;
    private BossStates previousEye;

    private float EyeLookSpeedDefault = 3;
    private float EyeLookSpeedTracking = 10;

    private float laserChargeTime = 3;
    private float laserChargeTimer;
    public GameObject Laser;

    private float eyeShootDelay = 1;
    private float eyeShootTimer;
    public GameObject Bullets;

    private float minionSpawnDelay = 3f;
    private float minionSpawnTimer;

    public int NextHealthTrigger;
    private int AmountOfCycles = 10;

    public bool CycleToNextEye = false;

    private float BossDiesFadeTime = 3;
    private float BossDiesFadeTimer;

    private float bossIsWaitingTime = 5;
    private float bossIsWaitingTimer;

    private FMODUnity.StudioEventEmitter AudioEmitter;
    public FMODUnity.EventReference[] SoundEffects;

    public override void Start()
    {
        CurrentBossState = 0;

        for (int i = 0; i < amountOfEyes; i++)
        {
            Eyes[i] = gameObject.transform.GetChild(i).GetComponent<EyeBossScript>();
        }
        AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        healthSlider.maxValue = bossMaxHealth;
    }

    public override void Update()
    {
        //Update the health slider
        healthSlider.value = BossHealth;
        healthSlider.transform.LookAt(Player.transform);

        switch (CurrentBossState)
        {
            //boss spawnes/resets state
            case BossStates.ResetState:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(true);
                    eye.renderer.material.color = Color.white;
                }
                BossHealth = bossMaxHealth;
                CycleToNextEye = false;

                AudioEmitter.EventReference = SoundEffects[0];
                AudioEmitter.Play();
                CurrentBossState = BossStates.WaitingState;

                break;

            //boss is waiting state
            case BossStates.WaitingState:
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
                    CurrentBossState = (BossStates)Random.Range(4, 5);
                    while (CurrentBossState == previousEye)
                    {
                        CurrentBossState = (BossStates)Random.Range(2, 5);
                    }
                    bossIsWaitingTimer = 0;
                }
                break;

            //boss uses left eye state
            case BossStates.LeftEyeState:
                Eyes[0].EyeIsActive = true;
                Eyes[0].lookSpeed = EyeLookSpeedTracking;
                previousEye = CurrentBossState;

                if (eyeShootTimer < eyeShootDelay)
                {
                    eyeShootTimer += Time.deltaTime;
                }
                else
                {
                    Instantiate(Bullets, bulletspawn.transform.position, bulletspawn.transform.rotation, transform);
                    eyeShootDelay = Random.Range(0.1f, 0.8f); //change shoot delay so that the shots are eratic instead of linear intervals

                    AudioEmitter.EventReference = SoundEffects[1];
                    AudioEmitter.Play();

                    eyeShootTimer = 0;
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    Eyes[0].lookSpeed = EyeLookSpeedDefault;
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss uses middle eye state
            case BossStates.MiddleEyeState:
                Eyes[1].EyeIsActive = true;
                previousEye = CurrentBossState;

                if (Minions != null && minionsSpawned == false)
                {
                    if (minionSpawnTimer < minionSpawnDelay)
                    {
                        minionSpawnTimer += Time.deltaTime;
                    }
                    else
                    {
                        Instantiate(Minions, MinionSpawnLocation.position, MinionSpawnLocation.rotation, MinionSpawnLocation);

                        AudioEmitter.EventReference = SoundEffects[2];
                        AudioEmitter.Play();

                        minionSpawnTimer = 0;
                    }
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    minionsSpawned = false;
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss uses right eye state
            case BossStates.RightEyeState:
                Eyes[2].EyeIsActive = true;
                Eyes[2].lookSpeed = EyeLookSpeedTracking;
                previousEye = CurrentBossState;

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
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss dies state;
            case BossStates.DieState:

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
                    CurrentBossState = BossStates.PrepareForIdleState;
                }

                break;

            //boss goes inactive state
            case BossStates.PrepareForIdleState:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(false);
                }

                BossDiesFadeTimer = 0;

                CurrentBossState = BossStates.IdleState;
                break;

            //boss is inactive state
            case BossStates.IdleState:
                break;
        }
    }
}
