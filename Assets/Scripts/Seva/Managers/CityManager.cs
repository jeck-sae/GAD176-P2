using UnityEngine;

public class CityManager : MonoBehaviour
{
    public City[] cities;
    public City currentCity = null;

    void Update()
    {
        CheckCityProximity();
    }

    void CheckCityProximity()
    {
        foreach (City city in cities)
        {
            if (Vector2.Distance(city.transform.position, PlayerManager.Instance.player.position) < city.spawnRadius)
            {
                if (currentCity != city)
                {
                    Debug.Log("Spawn");
                    if (currentCity != null) currentCity.DespawnNPCs();
                    currentCity = city;
                    currentCity.SpawnNPCs();
                }
                return;
            }
        }

        if (currentCity != null)
        {
            currentCity.DespawnNPCs();
            currentCity = null;
        }
    }
}
