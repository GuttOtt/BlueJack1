using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCover : MonoBehaviour {
	private Deck deck;

	private void Awake() {
		gameObject.AddComponent<BoxCollider2D>();
		
	}

	public void SetDeck(Deck deck) {
		this.deck = deck;
	}

	private void OnMouseDown() {
		deck.DrawDeckList();
	}
}
