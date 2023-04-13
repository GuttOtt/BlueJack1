using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalDamageEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private float ratio;

    public void Activate() {
        int pot = SnapManager.pot;
        float f = pot * ratio;
        int damage = (int) f;

        Card card = transform.parent.GetComponent<Card>();
        BlackJacker owner = card.owner;

        owner.DealDamage(damage);
    }
}
