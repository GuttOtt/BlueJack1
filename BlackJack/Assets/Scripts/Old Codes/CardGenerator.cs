using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour {
	public Card CreateCard(int number, Suit suit) {
		GameObject obj = new GameObject("Card");
		Card card = obj.AddComponent<Card>() as Card;
		card.Initialize(number, suit);

		return card;
	}
}
