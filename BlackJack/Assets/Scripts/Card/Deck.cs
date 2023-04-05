using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Deck : MonoBehaviour {
	[SerializeField] private GameObject deckParent;
	private List<Card> cards = new List<Card>();	
	private Hand hand;
	private Discards discards;

	private void Awake() {
		hand = transform.GetComponent<Hand>();
		discards = transform.GetComponent<Discards>();

		DeckCover deckCover = deckParent.AddComponent<DeckCover>();
		deckCover.SetDeck(this);
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
		card.MoveTo(deckParent.transform.position + Vector3.forward * (cards.Count+1));
	}

	public void Shuffle() {
		cards.Shuffle();
	}

	public void DrawDeckList() {
		List<CardData> data = new List<CardData>();
		foreach(Card card in cards) {
			data.Add(card.GetData());
		}
		DeckListUI.DrawDeckList(data);
	}
}

