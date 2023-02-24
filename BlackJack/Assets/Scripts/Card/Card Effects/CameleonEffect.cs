using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameleonEffect : MonoBehaviour, ICardEffect {

	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		Hand hand = card.owner.GetComponent<Hand>();

		Suit suit = hand.GetRandomField(card).GetSuit();
		card.ChangeSuit(suit);
	}
}
