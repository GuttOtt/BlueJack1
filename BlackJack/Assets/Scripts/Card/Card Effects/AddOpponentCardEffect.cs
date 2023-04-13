using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOpponentCardEffect : MonoBehaviour, ICardEffect {
    [SerializeField] CardIcon newIconPrefab;
    [SerializeField] int cardNumber;

    public void Activate() {
        Card card = transform.parent.GetComponent<Card>();
        BlackJacker opponent = card.owner.opponent;
        Hand opponentHand = opponent.GetComponent<Hand>();//this is a train wreck..

        GameObject obj = new GameObject("Card");
        Card newCard = obj.AddComponent<Card>() as Card;
        newCard.AddIcon(newIconPrefab);
        newCard.Initialize(cardNumber, (Suit)Random.Range(0, 4));
        newCard.owner = opponent;
        newCard.IsFront = true;

        opponentHand.AddCard(newCard);
    }
}
