using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public enum SoundType
{
    RifleShot,
    ShotGunShot,
    PistolShot,
    Reloading,
    Swing,
    Hit,
    Damage,
    Death,
    Item,
    FootStep
}
[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private SoundList[] soundList;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip clip = Instance.soundList[(int)sound].Sound;
        if (clip != null)
        {
            Instance.audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"Sound not found for: {sound}");
        }
    }
    public static void PlaySoundAtPoint(SoundType sound, Vector3 pos, float volume = 1)
    {
        AudioClip clip = Instance.soundList[(int)sound].Sound;
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, volume);
        }
        else
        {
            Debug.LogWarning($"Sound not found for: {sound}");
        }
    }
}
[Serializable]
public struct SoundList
{
    [SerializeField] private string name;
    [SerializeField] public AudioClip Sound;
}