using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSuitCondition : MonoBehaviour, IEffectCondition {
    [SerializeField] private int number;
    public bool IsSatisfied() {
        Card card = transform.parent.GetComponent<Card>();
        BlackJacker owner = GetComponent<CardIcon>().Owner;
        Hand hand = owner.GetComponent<Hand>();

        Debug.Log(card);

        return number <= hand.GetNumberOfSuitInField(card.GetSuit());
    }
}
