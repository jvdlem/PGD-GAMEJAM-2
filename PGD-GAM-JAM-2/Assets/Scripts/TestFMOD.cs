using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFMOD : MonoBehaviour
{

    FMOD.Studio.EventInstance Sound;

    void Start()
    {
        Sound = FMODUnity.RuntimeManager.CreateInstance("event:/MISC/Music/ShopMusic");
    }

    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }
    void OnTriggerEnter()
    {
        Sound.start();
    }

    void OnTriggerExit()
    {
        Sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //Sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
