using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : Singleton<DayNightCycle>
{
    public Light2D globalLight;
    public float dayDuration = 120f; // Duration of the day in seconds
    public float nightDuration = 60f; // Duration of the night in seconds
    public float TransitionTime = 30f; // Duration of the transition in seconds

    [Header("Colors")]
    public Color dayColor;
    public Color nightColor;
    public Color transitionColor;

    public float dayIntensity = 1f;
    public float nightIntensity = 0.6f;

    private float time;
    private bool isDayTime = true;
    public bool bloodyTime = false;

    void Update()
    {
        if (!bloodyTime)
        {
            time += Time.deltaTime;

            if (isDayTime)
            {
                if (time >= dayDuration - TransitionTime)
                {
                    float transitionProgress = (time - (dayDuration - TransitionTime)) / TransitionTime;

                    if (transitionProgress < 0.5f)
                    {
                        // Day to Transition
                        float subProgress = transitionProgress * 2f; // Normalize to [0, 1]
                        globalLight.color = Color.Lerp(dayColor, transitionColor, subProgress);
                    }
                    else
                    {
                        // Transition to Night
                        float subProgress = (transitionProgress - 0.5f) * 2f; // Normalize to [0, 1]
                        globalLight.color = Color.Lerp(transitionColor, nightColor, subProgress);
                        globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, subProgress);
                    }
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

                    if (transitionProgress < 0.5f)
                    {
                        // Night to Transition
                        float subProgress = transitionProgress * 2f; // Normalize to [0, 1]
                        globalLight.color = Color.Lerp(nightColor, transitionColor, subProgress);
                    }
                    else
                    {
                        // Transition to Day
                        float subProgress = (transitionProgress - 0.5f) * 2f; // Normalize to [0, 1]
                        globalLight.color = Color.Lerp(transitionColor, dayColor, subProgress);
                        globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, subProgress);
                    }
                }

                if (time >= nightDuration)
                {
                    isDayTime = true;
                    time = 0f;
                }
            }
        }
    }
}