using System.Collections.Generic;
using UnityEngine;

public class RandomEventTrigger : MonoBehaviour
{
    public float eventChance = 0.4f; // 1 is 100%
    protected bool wait = false;
    public List<RandomEvent> r_events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerEvent();
        }
    }
    void TriggerEvent()
    {
        if (!wait)
        {
            if (Random.value < eventChance)
            {
                // Randomly select an event
                int r = Random.Range(0, r_events.Count);
                r_events[r].triger = this;
                r_events[r].Initialize();

            }
            else
            {
                Debug.Log("No event");
            }
            wait = true;
            Invoke("Wait", 10f);
        }
    }
    public void SpawnEncounter(GameObject obj, float range)
    {
        //New destination
        float x = gameObject.transform.position.x + Random.Range(-range, range);
        float y = gameObject.transform.position.y + Random.Range(-range, range);
        Vector2 pos = new Vector2(x, y);

        Instantiate(obj, pos, Quaternion.identity);
    }
    void Wait()
    {
        wait = false;
    }
}
