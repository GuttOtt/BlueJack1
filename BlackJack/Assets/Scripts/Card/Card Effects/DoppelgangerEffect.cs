using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppelgangerEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private float ratio;
    public void Activate() {
        BlackJacker owner = GetComponent<CardIcon>().Owner;

        owner.DealDamage( (int) (owner.DamageDealtBeforeShowdown * ratio) );
    }
}
