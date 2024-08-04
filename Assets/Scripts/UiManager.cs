using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public TextMeshProUGUI spCount; 
    private int splitCountValue = 0;    
    public PlayerManager playerManager;


    public void UpdateSplitCount() {
       // Invoke("CheckUi", 0.02f);
        spCount.text = "Split Left: " + playerManager.GetAvailableSplits().ToString();
    }


    public void CheckUi() {

       
    }
    private void OnEnable() {
        ArrowController.onSplitHit += UpdateSplitCount;
    }
    private void OnDisable() {
        ArrowController.onSplitHit -= UpdateSplitCount;
    }
}
