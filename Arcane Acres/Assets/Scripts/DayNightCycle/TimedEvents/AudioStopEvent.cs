using UnityEngine;

public class AudioStopEvent : TimedEvent
{
    public AudioSource audioTrack;
    public float fadeDuration = 2.0f; // Duration of the fade effect in seconds

    protected override void OnTimeTriggered()
    {
        StartCoroutine(AudioFadeUtility.FadeOut(audioTrack, fadeDuration));
    }
}
