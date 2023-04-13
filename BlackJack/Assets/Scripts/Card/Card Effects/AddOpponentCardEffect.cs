using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOpponentCardEffect : MonoBehaviour, ICardEffect {
    [SerializeField] Card newCardPrefab;

    public void Activate() {
        Card card = transform.parent.GetComponent<Card>();
        BlackJacker opponent = card.owner.opponent;
        Hand hand = opponent.GetComponent<Hand>();

        Card newCard = Instantiate(newCardPrefab);
        newCard.owner = opponent;
        hand.AddCard(newCard);
    }

}
