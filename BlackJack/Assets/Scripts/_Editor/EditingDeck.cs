using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingDeck : MonoBehaviour {
	private List<EditingCard> cards = new List<EditingCard>();

	public void AddCard(EditingCard card) {
		cards.Add(card);
	}

	public void RemoveCard(EditingCard card) {
		cards.Remove(card);
	}
}
