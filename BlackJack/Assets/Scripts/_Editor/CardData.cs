using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData {
	[SerializeField] private int _number;
	[SerializeField] private Suit _suit;
	[SerializeField] private CardIcon _iconPrefab;
	public int number { get => _number; }
	public Suit suit { get => _suit; }
	public CardIcon iconPrefab { get => _iconPrefab; }
	public int ID { get => _iconPrefab.ID; }

	public CardData(int number, Suit suit, CardIcon iconPrefab) {
		_number = number;
		_suit = suit;
		_iconPrefab = iconPrefab;
	}

	public void PasteTo(EditingCard card) {
		card.ChangeNumber(number);
		card.ChangeSuit(suit);
		card.ChangeIcon(iconPrefab);
	}

	public void PasteTo(CopyCardImage img) {
		img.Draw(this);
	}

	public Card InstantiateAsCard() {
		return CardGenerator.CreateCard(number, suit, iconPrefab);
	}

	public void ChangeNumber(int number) {
		_number = number;
	}

	public void ChangeSuit(Suit suit) {
		_suit = suit;
	}

	public void ChangeIcon(CardIcon iconPrefab) {
		_iconPrefab = iconPrefab;
	}
}
