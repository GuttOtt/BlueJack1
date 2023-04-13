using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardEffect : MonoBehaviour, ICardEffect{
	[SerializeField] Card newCardPrefab;
    [SerializeField] int cardNumber;
    [SerializeField] Suit suit;

	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		Hand hand = card.owner.GetComponent<Hand>();

        Card newCard = Instantiate(newCardPrefab);
        newCard.owner = card.owner;
        hand.AddCard(newCard);
    }
}
