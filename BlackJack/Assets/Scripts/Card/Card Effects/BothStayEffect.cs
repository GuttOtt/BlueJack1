using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothStayEffect : MonoBehaviour, ICardEffect {
    public void Activate() {
        BlackJacker owner = GetComponent<CardIcon>().Owner;
        BlackJacker opponent = owner.opponent;

        owner.GetComponent<PlayerTC>().ForceToStay();
        opponent.GetComponent<PlayerTC>().ForceToStay();
    }
}
