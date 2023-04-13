using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnteDamageEffect : MonoBehaviour, ICardEffect {
    [SerializeField] private float ratio;

    public void Activate() {
        BlackJacker owner = GetComponent<CardIcon>().Owner;
        float damage = (float)SnapManager.ante * ratio;

        if (owner != null) {
            owner.DealDamage((int) damage);
        }
    }
}
