using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteContainer {
	public static Sprite NumberToSprite(int number) {
		return Resources.Load<Sprite>("Sprites/CardNumber"+number.ToString());
	}

	public static Sprite SuitToSprite(Suit suit) {
		Sprite sprite = null;
		switch (suit) {
			case Suit.Spade :
				sprite = Resources.Load<Sprite>("Sprites/CardSuitSpade");
				break;
			case Suit.Diamond :
				sprite = Resources.Load<Sprite>("Sprites/CardSuitDiamond");
				break;
			case Suit.Heart :
				sprite = Resources.Load<Sprite>("Sprites/CardSuitHeart");
				break;
			case Suit.Club :
				sprite = Resources.Load<Sprite>("Sprites/CardSuitClub");
				break;
		}

		return sprite;
	}
}