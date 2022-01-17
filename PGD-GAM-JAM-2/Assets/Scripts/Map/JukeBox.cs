using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    private int CurrentSongID;
    public FMODUnity.EventReference CurrentSong;
    public FMODUnity.EventReference[] BackGroundMusic;
    public FMODUnity.StudioEventEmitter AudioEmitter;

    private void Start()
    {
        CurrentSongID = 0;
        CurrentSong = BackGroundMusic[CurrentSongID];
        AudioEmitter.Play();
        if (AudioEmitter == null) AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Update()
    {
        if (!AudioEmitter.IsPlaying())
        {
            PlayNextSong();
        }
    }

    public void PlayNextSong()
    {
        AudioEmitter.Stop();
        AudioEmitter.Lookup();

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
        AudioEmitter.PlayInstance();
    }
}
