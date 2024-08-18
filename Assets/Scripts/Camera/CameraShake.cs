using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    public Animator anim;
    public void CamShake()
    {
        anim.SetTrigger("Shake");
    }
}// Animation that shakes the camera
