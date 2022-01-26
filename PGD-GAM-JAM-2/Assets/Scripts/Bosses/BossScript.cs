using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : EnemyBaseScript
{
    //Boss States
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

    //The 3 Boss eye's
    private static int amountOfEyes = 3;
    private EyeBossScript[] Eyes = new EyeBossScript[amountOfEyes];

    //Boss Health Values
    private int bossMaxHealth = 10;
    public int BossHealth;
    public Slider healthSlider;

    //The eye that was active last
    private BossStates previousEye;

    //Tracking speed of the eyes, can change depending on attack.
    private float EyeLookSpeedDefault = 3;
    private float EyeLookSpeedTracking = 10;

    //Big Laser charge values
    private float laserChargeTime = 3;
    private float laserChargeTimer;
    private float laserChargeColorSpeed = 3;
    public GameObject Laser;

    //Small Laser shooting values
    private float waitBeforefirstShot = 1, eyeShootDelay = 1; //Need to be the same value
    private float eyeShootTimer;
    private float eyeShootDelayMinimum = 0.1f;
    private float eyeShootDelayMaximum = 0.8f;
    public GameObject Bullets;
    public GameObject bulletspawn;

    //Minion Spawning values
    private float minionSpawnDelay = 3f;
    private float minionSpawnTimer;
    public GameObject Minions;
    public Transform MinionSpawnLocation;

    //Boss Health Cycle values
    public int NextHealthTrigger;
    private int AmountOfCycles = 10;

    //Boss dies fade animation values
    private float BossDiesFadeTime = 3;
    private float BossDiesFadeTimer;

    //Boss is waiting for next attack cycle values
    private float bossIsWaitingTime = 5;
    private float bossIsWaitingTimer;
    public bool CycleToNextEye = false;

    //FMOD Values
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

        //The switch statement for the boss state machine
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

                //Play spawn Sound Effect
                AudioEmitter.EventReference = SoundEffects[0];
                AudioEmitter.Play();
                CurrentBossState = BossStates.WaitingState;
                break;

            //Boss is waiting state
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
                    //Set boss state to random eye attack, check if the new AttackingState isnt the same as the old AttackingState
                    CurrentBossState = (BossStates)Random.Range(2, 5);
                    while (CurrentBossState == previousEye)
                    {
                        CurrentBossState = (BossStates)Random.Range(2, 5);
                    }
                    bossIsWaitingTimer = 0;
                }
                break;

            //Boss uses left eye state - small lasers
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

                    //Play small laser shoot sound effect
                    AudioEmitter.EventReference = SoundEffects[1];
                    AudioEmitter.Play();

                    eyeShootTimer = 0;
                }

                //Reset this eye behaviour and go to WaitingState
                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    eyeShootDelay = waitBeforefirstShot; //Reset Delay to the first float given so that next cycle the eye will wait again before attacking
                    CurrentActiveEye.lookSpeed = EyeLookSpeedDefault;
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //boss uses middle eye state - spawn minions
            case BossStates.MiddleEyeState:
                CurrentActiveEye = Eyes[1];

                CurrentActiveEye.EyeIsActive = true;
                previousEye = CurrentBossState;

                if (Minions != null)
                {
                    //Spawn a single minion every x seconds, x being minionSpawnDelay
                    if (minionSpawnTimer < minionSpawnDelay)
                    {
                        minionSpawnTimer += Time.deltaTime;
                    }
                    else
                    {
                        //Spawn Minion
                        Instantiate(Minions, MinionSpawnLocation.position, MinionSpawnLocation.rotation, MinionSpawnLocation);

                        //Play Minion spawn sound effect
                        AudioEmitter.EventReference = SoundEffects[2];
                        AudioEmitter.Play();

                        minionSpawnTimer = 0;
                    }
                }

                //Reset this eye behaviour and go to WaitingState
                if (CycleToNextEye)
                {
                    CycleToNextEye = false;
                    CurrentBossState = BossStates.WaitingState;
                }
                break;

            //Boss uses right eye state - charge big laser
            case BossStates.RightEyeState:
                CurrentActiveEye = Eyes[2];

                CurrentActiveEye.EyeIsActive = true;
                CurrentActiveEye.lookSpeed = EyeLookSpeedTracking;
                previousEye = CurrentBossState;

                //Charge laser and change the color of the eye to increasingly red
                if (laserChargeTimer < laserChargeTime)
                {
                    laserChargeTimer += Time.deltaTime;
                    CurrentActiveEye.renderer.material.color = Color.Lerp(Color.white, Color.red, laserChargeTimer / laserChargeColorSpeed);
                }
                else
                {
                    //Spawn big laser if charge time is complete
                    if (CurrentActiveEye.transform.childCount == 0)
                    {
                        Instantiate(Laser, CurrentActiveEye.transform.position, Quaternion.Euler(CurrentActiveEye.transform.rotation.eulerAngles.x - 90, CurrentActiveEye.transform.rotation.eulerAngles.y, CurrentActiveEye.transform.rotation.eulerAngles.z), CurrentActiveEye.transform);

                        //Play Big Laser sound effect
                        AudioEmitter.EventReference = SoundEffects[3];
                        AudioEmitter.Play();
                    }
                    CurrentActiveEye.renderer.material.color = Color.red;
                }

                //Reset this eye behaviour and go to WaitingState
                if (CycleToNextEye)
                {
                    CycleToNextEye = false;

                    //Delete the big laser if it is present
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

                //Play boss dies sound effect
                AudioEmitter.EventReference = SoundEffects[4];
                AudioEmitter.Play();

                //Delete the big laser if it is still active
                if (Eyes[2].transform.childCount != 0)
                {
                    Destroy(Eyes[2].transform.GetChild(0).gameObject);
                }

                //Fade de boss transparant, then go to PrepareForIdleState
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
                    BossDiesFadeTimer = 0;
                    CurrentBossState = BossStates.PrepareForIdleState;
                }

                break;

            //boss goes inactive state
            case BossStates.PrepareForIdleState:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(false);
                }
                healthSlider.gameObject.SetActive(false);

                CurrentBossState = BossStates.IdleState;
                break;

            //boss is inactive state
            case BossStates.IdleState:
                break;
        }
    }
}
