using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherTotalCondition : MonoBehaviour, IEffectCondition {
    public bool IsSatisfied() {
        BlackJacker owner = GetComponent<CardIcon>().Owner;
        BlackJacker opponent = owner.opponent;
        Hand ownerHand = owner.GetComponent<Hand>();
        Hand opponentHand = opponent.GetComponent<Hand>();

        return opponentHand.GetFieldTotal() < ownerHand.GetFieldTotal();
    }
}
