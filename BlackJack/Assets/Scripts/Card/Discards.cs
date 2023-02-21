using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discards : MonoBehaviour {
	[SerializeField] private GameObject discardsParent;
	private Deck deck;
	private List<Card> cards = new List<Card>();

	private void Awake() {
		deck = GetComponent<Deck>();
	}

	public void AddCard(Card card) {
		cards.Add(card);
		card.transform.SetParent(discardsParent.transform);
		card.MoveTo(discardsParent.transform.position + cards.Count * Vector3.back);
		card.IsFront = true;
	}

	public void AllToDeck() {
		for (int i = cards.Count - 1; i >= 0; i--) {
			Card card = cards[i];
			deck.AddCard(card);
			cards.RemoveAt(i);
			deck.Shuffle();
		}
	}
}