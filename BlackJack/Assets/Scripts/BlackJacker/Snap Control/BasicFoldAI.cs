using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFoldAI : MonoBehaviour, IFoldAI {
    private Hand hand;

    private void Awake() {
        hand = GetComponent<Hand>();
    }

    public bool DecideFold() {
        int total = hand.GetTotal();
        int r = Random.Range(0, 100);

        if (!SnapManager.isPlayerSnaped) {
            return false;
        }
        
        if (total == 16) {
            if (r < 10) return true;
        }
        else if (total == 18) {
            if (r < 50) return true;
        }
        else if (total == 19) {
            if (r < 70) return true;
        }

        return false;
    }
}
