using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeEvent : TimedEvent
{
    public AudioSource musicTrack;
    public float fadeDuration = 2.0f; // Duration of the fade effect in seconds

    protected override void OnTimeTriggered()
    {
        StartCoroutine(AudioFadeUtility.FadeIn(musicTrack, fadeDuration));
    }
}
