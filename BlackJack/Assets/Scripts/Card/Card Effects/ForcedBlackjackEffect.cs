using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedBlackjackEffect : MonoBehaviour, ICardEffect {
    public void Activate() {
        BlackJacker owner = GetComponent<CardIcon>().Owner;
        Hand hand = owner.GetComponent<Hand>();

        hand.ForceToBlackjack();
    }
}
