using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private int baseDamage;

    public void Activate() {
        CardIcon icon = GetComponent<CardIcon>();
        BlackJacker owner = icon.Owner;

        owner.DealDamage(baseDamage * (owner.NumberOfAcceleration + 1));
        owner.NumberOfAcceleration++;
    }
}
