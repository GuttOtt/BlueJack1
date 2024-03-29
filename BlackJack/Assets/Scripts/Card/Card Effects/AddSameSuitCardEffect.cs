using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSameSuitCardEffect : MonoBehaviour, ICardEffect {
    [SerializeField] CardIcon newIconPrefab;

    public void Activate() {
        Card card = transform.parent.GetComponent<Card>();
        Hand hand = card.owner.GetComponent<Hand>();

        GameObject obj = new GameObject("Card");
        Card newCard = obj.AddComponent<Card>();
        newCard.AddIcon(newIconPrefab);
        newCard.Initialize(0, card.GetSuit());
        newCard.owner = card.owner;
        newCard.IsFront = true;

        hand.AddCard(newCard);
    }
}
