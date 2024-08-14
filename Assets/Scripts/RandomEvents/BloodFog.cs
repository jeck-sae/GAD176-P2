using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFog : MonoBehaviour
{
    public ParticleSystem rainEffect;
    public ParticleSystem bloodFogEffect;
    public GameObject MonstraPrefab;
    public float transitionDelay = 5f; // Time before transitioning from rain to bloodfog
    public float eventDuration = 60f; // Duration of the event
    public float endingDuration = 10f;// Duration of the end stage
    public float spawnDistance = 160f;// Distance from player to spawn the monstra
    public float minTimeBetweenEvents = 60f;
    public float maxTimeBetweenEvents = 180f;

    private Transform player;
    private GameObject activeMonstra;
    private bool BloodFofActive = false;
    private float nextEventTime;

    void Start()
    {
        player = PlayerManager.Instance.player;
        rainEffect.Stop();
        bloodFogEffect.Stop();
        ScheduleNextEvent();
    }

    void Update()
    {
        if (!BloodFofActive && Time.time >= nextEventTime)
        {
            StartBloodFogEvent();
        }
    }

    void ScheduleNextEvent()
    {
        nextEventTime = Time.time + Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
    }

    void StartBloodFogEvent()
    {
        BloodFofActive = true;
        //DayNightCycle.Instance.stop = true;
        StartRain();
        AudioManager.PlaySound(SoundType.Rain, 0.2f);
        Debug.Log("Start");
    }

    void StartRain()
    {
        rainEffect.Play();
        Invoke("StartBloodFog", transitionDelay);
    }

    void StartBloodFog()
    {
        rainEffect.Stop();
        bloodFogEffect.Play();
        AudioManager.StopMusicGradually();

        // Spawn the monstra after the bloodfog starts
        SpawnMonstra();

        // End the event after a set duration
        Invoke("End", eventDuration);
    }

    void SpawnMonstra()
    {
        Vector3 spawnPosition = GetPosOutsideView();
        activeMonstra = Instantiate(MonstraPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawn");
    }

    Vector3 GetPosOutsideView()
    {
        Vector3 spawnPosition;
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;

        // Calculate spawn position at a distance from the player
        spawnPosition = player.position + direction * spawnDistance;

        return spawnPosition;
    }
    void End()
    {
        Invoke("EndBloodFogEvent", endingDuration);
        if (activeMonstra != null)
        {
            activeMonstra.GetComponent<MonstraAI>().Leave();
        }
    }
    void EndBloodFogEvent()
    {
        bloodFogEffect.Stop();

        // Destroy monstra
        if (activeMonstra != null)
        {
            Destroy(activeMonstra);
        }

        // Clear the event and schedule the next one
        BloodFofActive = false;
        //DayNightCycle.Instance.stop = false;
        ScheduleNextEvent();
    }
}
