using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource tetris_song;
    public static bool musicEnabled = true;
    public static float volumeLevel = 1;

    // Update is called once per frame
    void Update()
    {
        tetris_song.GetComponent<AudioSource>().mute = !musicEnabled;
        tetris_song.GetComponent<AudioSource>().volume = volumeLevel;
    }
}

