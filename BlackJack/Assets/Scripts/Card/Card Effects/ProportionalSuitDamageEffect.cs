using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalSuitDamageEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private float ratio;
    public void Activate() {
        Card card = transform.parent.GetComponent<Card>();
        BlackJacker owner = card.owner;
        Hand hand = owner.GetComponent<Hand>();

        int numberOfSuit = hand.GetNumberOfSuitInField(card.GetSuit());
        float damage = numberOfSuit * ratio;

        owner.DealDamage( (int) damage);
    }
}
