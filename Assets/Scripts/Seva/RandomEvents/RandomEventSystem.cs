using System.Collections;
using UnityEngine;

public class RandomEventSystem : MonoBehaviour
{
    public float eventChance = 0.2f; // 20% chance of an event
    public Transform[] possibleEvents; // Prefabs or event handlers for different events

    void TriggerEvent()
    {
        if (Random.value < eventChance)
        {
            // Randomly select an event
            int eventIndex = Random.Range(0, possibleEvents.Length);
            Instantiate(possibleEvents[eventIndex], transform.position, Quaternion.identity);

            // Example of specific event logic, like spawning a robber
            Debug.Log("You are attacked by a robber!");
        }
        else
        {
            Debug.Log("No event occurred.");
        }
    }

    // Call this method when the player moves to a new area
    public void OnPlayerMove()
    {
        TriggerEvent();
    }
}
