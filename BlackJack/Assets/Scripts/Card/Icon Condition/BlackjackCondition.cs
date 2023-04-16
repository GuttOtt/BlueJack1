using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackCondition : MonoBehaviour, IEffectCondition { 
    public bool IsSatisfied() {
        CardIcon icon = GetComponent<CardIcon>();
        Hand hand = icon.Owner.GetComponent<Hand>();

        return hand.GetTotal() == 21;
    }
}
