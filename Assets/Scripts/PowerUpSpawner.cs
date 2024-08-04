using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {
    public GameObject[] powerUpPrefabs;
    public Transform[] spawnPoints; 
    public float minSpawnInterval = 5f; 
    public float maxSpawnInterval = 10f; 
    public float powerUpLifeTime = 10f;
    public int maxSpawnsPerPowerUp = 3; 

    private GameObject currentPowerUp = null;
    private Dictionary<GameObject, int> powerUpSpawnCounts = new Dictionary<GameObject, int>(); 

    void Start() {
       
        foreach (var powerUpPrefab in powerUpPrefabs) {
            powerUpSpawnCounts[powerUpPrefab] = 0;
        }
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps() {
        while (true) {
            if (currentPowerUp == null) {
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

                List<GameObject> availablePowerUps = new List<GameObject>();

               
                foreach (var powerUpPrefab in powerUpPrefabs) {
                    if (powerUpSpawnCounts[powerUpPrefab] < maxSpawnsPerPowerUp) {
                        availablePowerUps.Add(powerUpPrefab);
                    }
                }

              
                if (availablePowerUps.Count == 0) {
                    yield break;
                }

                int spawnIndex = Random.Range(0, spawnPoints.Length);
                int powerUpIndex = Random.Range(0, availablePowerUps.Count);
                GameObject selectedPowerUp = availablePowerUps[powerUpIndex];

                currentPowerUp = Instantiate(selectedPowerUp, spawnPoints[spawnIndex].position, Quaternion.identity);
                powerUpSpawnCounts[selectedPowerUp]++;
                StartCoroutine(DestroyAfterTime(currentPowerUp, powerUpLifeTime));
            }
            else {
                yield return null;
            }
        }
    }

    IEnumerator DestroyAfterTime(GameObject powerUp, float delay) {
        yield return new WaitForSeconds(delay);
        if (powerUp != null) {
            Destroy(powerUp);
            currentPowerUp = null;
        }
    }
}
