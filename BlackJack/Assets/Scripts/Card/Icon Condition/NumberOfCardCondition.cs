using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfCardCondition : MonoBehaviour, IEffectCondition {
    [SerializeField] private bool isEqualIncluded;
    [SerializeField] private bool isLessThanCondition;
    [SerializeField] private bool isOpponentHandCondition;
    [SerializeField] private int number;

    public bool IsSatisfied() {
        BlackJacker target = GetComponent<CardIcon>().Owner;
        if (isOpponentHandCondition) target = target.opponent;

        Hand hand = target.GetComponent<Hand>();
        bool b;
        if (isLessThanCondition) b = hand.GetNumberOfCard() < number;
        else b = hand.GetNumberOfCard() > number;

        if (isEqualIncluded && hand.GetNumberOfCard() == number) b = true;

        return b;
    }
}
