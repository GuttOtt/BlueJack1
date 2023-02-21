using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour {
	private List<Card> cards = new List<Card>();	
	private Hand hand;
	private Discards discards;
	[SerializeField] private GameObject deckParent;

	private void Awake() {
		hand = transform.GetComponent<Hand>();
		discards = transform.GetComponent<Discards>();
	}

	private Card Draw() {
		if (cards.Count < 1) {
			discards.AllToDeck();
			Shuffle();
		}

		Card card = cards[cards.Count -1];
		cards.Remove(card);
		return card;
	}

	public void ClosedHit() {
		Card card = Draw();
		hand.AddHidden(card);
	}

	public void OpenHit() {
		Card card = Draw();
		hand.AddCard(card);
	}

	public void AddCard(Card card) {
		cards.Add(card);
		card.IsFront = false;
		card.transform.SetParent(deckParent.transform);
		card.MoveTo(deckParent.transform.position);
	}
	public void Shuffle() {
		cards.Shuffle();
	}

	
}

