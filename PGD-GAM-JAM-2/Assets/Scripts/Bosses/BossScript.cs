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

    private BossStates previousEye;

    private float EyeLookSpeedDefault = 3;
    private float EyeLookSpeedTracking = 10;

    private float laserChargeTime = 3;
    private float laserChargeTimer;
    private float laserChargeColorSpeed = 3;
    public GameObject Laser;

    private float eyeShootDelay = 1;
    private float eyeShootTimer;
    private float eyeShootDelayMinimum = 0.1f;
    private float eyeShootDelayMaximum = 0.8f;
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
                    CurrentBossState = (BossStates)Random.Range(2, 5);
                    while (CurrentBossState == previousEye)
                    {
                        CurrentBossState = (BossStates)Random.Range(2, 5);
                    }
                    bossIsWaitingTimer = 0;
                }
                break;

            //boss uses left eye state
            case BossStates.LeftEyeState:
                EyeBossScript CurrentActiveEye = Eyes[0];

                CurrentActiveEye.EyeIsActive = true;
                CurrentActiveEye.lookSpeed = EyeLookSpeedTracking;
                previousEye = CurrentBossState;

                if (eyeShootTimer < eyeShootDelay)
                {
                    eyeShootTimer += Time.deltaTime;
                }
                else
                {
                    Instantiate(Bullets, bulletspawn.transform.position, bulletspawn.transform.rotation, transform);
                    eyeShootDelay = Random.Range(eyeShootDelayMinimum, eyeShootDelayMaximum); //change shoot delay so that the shots are eratic instead of linear intervals

                    AudioEmitter.EventReference = SoundEffects[1];
                    AudioEmitter.Play();

                    eyeShootTimer = 0;
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    CurrentActiveEye.lookSpeed = EyeLookSpeedDefault;
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss uses middle eye state
            case BossStates.MiddleEyeState:
                CurrentActiveEye = Eyes[1];

                CurrentActiveEye.EyeIsActive = true;
                previousEye = CurrentBossState;

                if (Minions != null)
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
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss uses right eye state
            case BossStates.RightEyeState:
                CurrentActiveEye = Eyes[2];

                CurrentActiveEye.EyeIsActive = true;
                CurrentActiveEye.lookSpeed = EyeLookSpeedTracking;
                previousEye = CurrentBossState;

                if (laserChargeTimer < laserChargeTime)
                {
                    laserChargeTimer += Time.deltaTime;
                    CurrentActiveEye.renderer.material.color = Color.Lerp(Color.white, Color.red, laserChargeTimer / laserChargeColorSpeed);
                }
                else
                {
                    if (CurrentActiveEye.transform.childCount == 0)
                    {
                        Instantiate(Laser, CurrentActiveEye.transform.position, Quaternion.Euler(CurrentActiveEye.transform.rotation.eulerAngles.x - 90, CurrentActiveEye.transform.rotation.eulerAngles.y, CurrentActiveEye.transform.rotation.eulerAngles.z), CurrentActiveEye.transform);

                        AudioEmitter.EventReference = SoundEffects[3];
                        AudioEmitter.Play();
                    }
                    CurrentActiveEye.renderer.material.color = Color.red;
                }

                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    if (CurrentActiveEye.transform.childCount != 0)
                    {
                        Destroy(CurrentActiveEye.transform.GetChild(0).gameObject);
                        AudioEmitter.Stop();
                    }
                    CurrentActiveEye.lookSpeed = EyeLookSpeedDefault;
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
                            BossDiesFadeTimer / 2);
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
