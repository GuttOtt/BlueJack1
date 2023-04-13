using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private int damage;

    public void Activate() {
        Card card = transform.parent.GetComponent<Card>();
        BlackJacker blackJacker = card.GetComponent<BlackJacker>();
        if (blackJacker != null) {

            blackJacker.DealDamage(damage);
        }
    }
}
