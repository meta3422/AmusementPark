using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChannel : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;

    public bool IsPlaying
    {
        get => audioSource.isPlaying;
    }

    public bool Loop
    {
        get => audioSource.loop;
        set => audioSource.loop = value;
    }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        gameObject.name = "Channel";
    }

    private void OnEnable()
    {
        if (audioClip) audioSource.PlayOneShot(audioClip);
    }

    private void Update()
    {
        if (audioSource.loop) return;
        if (!audioSource.isPlaying) SoundManager.Instance.WithdrawChannel(this);
    }

    public void Play()
    {
        if (audioSource == null) return;
        if (audioClip == null) return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
