using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 10;
    public float spawnInterval = 6f;
    public Slider progressSlider; 

    private int waveCount = 0;
    private int enemiesSpawned = 0; 

    void Start() {
        
        if (progressSlider != null) {
            progressSlider.maxValue = enemiesPerWave;
            progressSlider.value = 0;
        }

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave() {
        for (int i = 0; i < enemiesPerWave; i++) {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);

            
            enemiesSpawned++;
            if (progressSlider != null) {
                progressSlider.value = enemiesSpawned;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
        waveCount++;
       
    }
}
