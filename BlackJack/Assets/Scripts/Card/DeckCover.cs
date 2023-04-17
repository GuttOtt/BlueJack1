using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCover : MonoBehaviour {
	[SerializeField] private Deck deck;

	private void Awake() {
		
	}

	private void OnMouseDown() {
		deck.DrawDeckList();
	}
}
