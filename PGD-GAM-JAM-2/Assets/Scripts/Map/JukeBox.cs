using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    private int CurrentSongID;
    public FMODUnity.EventReference CurrentSong;
    public FMODUnity.EventReference[] BackGroundMusic;
    public FMODUnity.StudioEventEmitter AudioEmitter;

    public Elevator elevator;

    private void Start()
    {
        CurrentSongID = 0;
        CurrentSong = BackGroundMusic[CurrentSongID];
        if (AudioEmitter == null) AudioEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        AudioEmitter.EventReference = CurrentSong;
        AudioEmitter.Play();
    }

    private void Update()
    {
        if (elevator.PlayerInElevator == false)
        {
            if (AudioEmitter.IsPlaying() == false)
            {
                PlayNextSong();
            }
        }
        else AudioEmitter.Stop();
        
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
}
