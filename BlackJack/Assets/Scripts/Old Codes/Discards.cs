using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discards : MonoBehaviour {
	[SerializeField] private GameObject discardsParent;
	[SerializeField] private Deck deck;
	private List<Card> cards = new List<Card>();

	public void AddCard(Card card) {
		cards.Add(card);
		card.transform.SetParent(discardsParent.transform);
		card.transform.localPosition = Vector2.zero;
	}

	public void AllToDeck() {
		foreach (Card card in cards) {
			deck.AddCard(card);
			cards.Remove(card);
		}
	}
}