using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public Light2D globalLight;
    public float dayDuration = 120f; // Duration of the day in seconds
    public float nightDuration = 60f; // Duration of the night in seconds
    public float TransitionTime = 30f;

    public Color dayColor;
    public Color nightColor;

    public float dayIntensity = 1f;
    public float nightIntensity = 0.6f;

    private float time;
    private bool isDayTime = true;

    void Update()
    {
        time += Time.deltaTime;

        if (isDayTime)
        {
            if (time >= dayDuration - TransitionTime)
            {
                float transitionProgress = (time - (dayDuration - TransitionTime)) / TransitionTime;
                globalLight.color = Color.Lerp(dayColor, nightColor, transitionProgress);
                globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, transitionProgress);
            }

            if (time >= dayDuration)
            {
                isDayTime = false;
                time = 0f;
            }
        }
        else
        {
            if (time >= nightDuration - TransitionTime)
            {
                float transitionProgress = (time - (nightDuration - TransitionTime)) / TransitionTime;
                globalLight.color = Color.Lerp(nightColor, dayColor, transitionProgress);
                globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, transitionProgress);
            }

            if (time >= nightDuration)
            {
                isDayTime = true;
                time = 0f;
            }
        }
    }
}