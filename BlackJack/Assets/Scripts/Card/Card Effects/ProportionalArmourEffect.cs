using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalArmourEffect : MonoBehaviour, ICardEffect{
    [SerializeField] float ratio;
    public void Activate() {
        int pot = SnapManager.pot;
        float f = pot * ratio;
        int amount = (int)f;

        BlackJacker owner = GetComponent<CardIcon>().Owner;

        owner.GainArmour(amount);
    }
}
