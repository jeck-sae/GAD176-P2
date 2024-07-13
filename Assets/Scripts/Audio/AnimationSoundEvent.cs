using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundEvent : MonoBehaviour
{
    public SoundType Type;
    public float volume;
    public void PlaySound()
    {
        AudioManager.PlaySound(Type, volume);
    }
}
