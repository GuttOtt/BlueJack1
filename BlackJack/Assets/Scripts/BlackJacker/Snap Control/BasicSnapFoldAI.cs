using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSnapFoldAI : MonoBehaviour, ISnapFoldAI {
    private Hand hand;

    private void Awake() {
        hand = GetComponent<Hand>();
    }

    public SnapDecision Decide() {
        int total = hand.GetTotal();
        
        int r = Random.Range(0, 100);
        if (total <= 15) {
            if (r < 5) return SnapDecision.Snap;
        }
        else if (total <= 16) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 50) return SnapDecision.Snap;
                else if (r < 15) return SnapDecision.Fold;
            }
            else {
                if (r < 25) return SnapDecision.Snap;
            }
        }
        else if (total <= 17) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 100) return SnapDecision.Snap;
            }
            else {
                if (r < 20) return SnapDecision.Snap;
            }
        }
        else if (total <= 18) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 5) return SnapDecision.Snap;
                else if (r < 55) return SnapDecision.Fold;
            }
        }
        else if (total <= 19) {
            if (!SnapManager.isPlayerSnaped) {
                if (r < 5) return SnapDecision.Snap;
                else if (r < 55) return SnapDecision.Fold;
            }
        }
        else if (total <= 21) {
            if (r < 100) return SnapDecision.Snap;
        }
        return SnapDecision.None;
    } 
}
