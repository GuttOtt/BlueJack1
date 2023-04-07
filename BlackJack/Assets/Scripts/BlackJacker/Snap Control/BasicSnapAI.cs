using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSnapAI : MonoBehaviour, ISnapAI {
    private Hand hand;

    private void Awake() {
        hand = GetComponent<Hand>();
    }

    public bool DecideSnap() {
        int total = hand.GetTotal();
        
        if (SnapManager.isEnemySnaped) {
            return false;
        }
        
        int r = Random.Range(0, 100);
        if (total <= 15) {
            if (r < 5) return true;
        }
        else if (total <= 16) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 50) return true;
            }
            else {
                if (r < 25) return true;
            }
        }
        else if (total <= 17) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 100) return true;
            }
            else {
                if (r < 20) return true;
            }
        }
        else if (total <= 19) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 5) return true;
            }
        }
        else if (total <= 21) {
            if (r < 100) return true;
        }
        return false;
    } 
}
