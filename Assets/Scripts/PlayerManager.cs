using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject playerPrefab;  
    public Transform[] spawnPoints;  
    public TextMeshProUGUI spCount;
    private float AvailableSplits;
    private int maxSplits;
    private void OnEnable() {
        ArrowController.onSplitHit += IncrementSplitCount;
        ArrowController.onHealthHit += IncreasePlayerHealth;
    }
    void Start() {
        AvailableSplits = 0;
         maxSplits = 3;

       
        GameObject player = Instantiate(playerPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
       

        Debug.Log("Initial splitCount: " + AvailableSplits);
    }

    public void SplitPlayer() {
        if (maxSplits > 0 && AvailableSplits > 0) {
            GameObject newPlayer = null;
            int spawnIndex = 0;

            if (maxSplits == 3) {
                spawnIndex = 1;
            }
            else if (maxSplits == 2) {
                spawnIndex = 2;
            }
            else if (maxSplits == 1) {
                spawnIndex = 3;
            }

            if (spawnIndex < spawnPoints.Length) {
                newPlayer = Instantiate(playerPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

             
                Debug.Log($"Player {4 - maxSplits} spawned.");

                if (newPlayer != null) {
                    PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();

                    if (newPlayerController != null) {
                        if (maxSplits == 3) {
                            newPlayerController.SetPlayer2();
                        }
                        else if (maxSplits == 2) {
                            newPlayerController.SetPlayer3();
                        }
                        else if (maxSplits == 1) {
                            newPlayerController.SetPlayer4();
                        }
                    }
                    else {
                        Debug.LogError("PlayerController component not found on instantiated player.");
                    }
                }

                maxSplits--;
                AvailableSplits--;
                Debug.Log("After SplitPlayer, splitCount: " + AvailableSplits);
            }
            else {
                Debug.LogError("Not enough spawn points available.");
            }
        }
        else {
            Debug.Log("Can't split more players.");
        }
    }


    public void IncreasePlayerHealth() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.IncreaseHealth(30f);
            }
        }
    }


    public void IncrementSplitCount() {
        AvailableSplits++;
      

    }

    public float GetAvailableSplits() {
        return AvailableSplits;
    }
    private void OnDisable() {
        ArrowController.onSplitHit -= IncrementSplitCount;
        ArrowController.onHealthHit -= IncreasePlayerHealth;
    }
}
