using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData {
	[SerializeField] private int number;
	[SerializeField] private Suit suit;
	[SerializeField] private CardIcon iconPrefab;

	public CardData(int number, Suit suit, CardIcon iconPrefab) {
		this.number = number;
		this.suit = suit;
		this.iconPrefab = iconPrefab;
	}

	public void PasteTo(EditingCard card) {
		card.ChangeNumber(number);
		card.ChangeSuit(suit);
		card.ChangeIcon(iconPrefab);
	}

	public void PasteTo(Card card) {

	}

	public void PasteTo(CopyCardImage img) {
		img.Draw(number, suit, iconPrefab);
	}

	public Card InstantiateAsCard() {
		GameObject obj = new GameObject("Card");
		Card card = obj.AddComponent<Card>() as Card;
		card.AddIcon(iconPrefab);
		card.Initialize(number, suit);

		return card;
	}

	public void ChangeNumber(int number) {
		this.number = number;
	}

	public void ChangeSuit(Suit suit) {
		this.suit = suit;
	}

	public void ChangeIcon(CardIcon iconPrefab) {
		this.iconPrefab = iconPrefab;
	}
}
