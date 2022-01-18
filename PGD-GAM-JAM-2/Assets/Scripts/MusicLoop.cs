using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    private int CurrentSongID;
    public FMODUnity.EventReference CurrentSong;
    public FMODUnity.EventReference[] BackGroundMusic;
    public FMODUnity.StudioEventEmitter AudioEmitter;

    public bool loop = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSongID = 0;
        CurrentSong = BackGroundMusic[CurrentSongID];
        if (AudioEmitter == null) AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        AudioEmitter.EventReference = CurrentSong;
    }

    // Update is called once per frame
    void Update()
    {
        if (loop)
        {
            if (AudioEmitter.IsPlaying() == false)
            {
                PlayNextSong();
            }
        }

        if (AudioEmitter.IsPlaying() == true) loop = true;
    }

    public void PlayNextSong()
    {
        AudioEmitter.Stop();

        if (CurrentSongID == BackGroundMusic.Length - 1)
        {
            CurrentSongID = 0;
        }
        else
        {
            CurrentSongID++;
        }

        CurrentSong = BackGroundMusic[CurrentSongID];

        AudioEmitter.EventReference = CurrentSong;

        AudioEmitter.Lookup();
        AudioEmitter.PlayInstance();
    }

    public void loopMusic(bool loopTheMusic)
    {
        loop = loopTheMusic;
    }
}