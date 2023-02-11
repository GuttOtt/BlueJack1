using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card: MonoBehaviour {
	private int number;
	private Suit suit;

	public void Initialize(int number, Suit suit) {
		DrawBlank();
		this.number = number;
		DrawNumberSprite();
		this.suit = suit;
		DrawSuitSprite();
	}

	public bool IsEqualNumber(int number) {
		return this.number == number;
	}
	
	public bool IsEqualSuit(Suit suit) {
		return this.suit == suit;
	}

	public int GetNumber() {
		return number;
	}

	private void DrawNumberSprite() {
		Sprite sprite = null;

		switch (number) {
			case 1:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber1");
				break;
			case 2:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber2");
				break;
			case 3:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber3");
				break;
			case 4:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber4");
				break;
			case 5:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber5");
				break;
			case 6:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber6");
				break;
			case 7:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber7");
				break;
		}

		GameObject obj = new GameObject("CardNumber");
		SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
		spr.sprite = sprite;
		obj.transform.SetParent(this.transform);
		obj.transform.localPosition = Vector3.zero + new Vector3(-0.8f, 1f, -1f);
	}

	private void DrawSuitSprite() {
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
		
		GameObject obj = new GameObject("CardSuit");
		SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
		spr.sprite = sprite;
		obj.transform.SetParent(this.transform);
		obj.transform.localPosition = Vector3.zero + new Vector3(-0.3f, 1f, -1f);
	}

	private void DrawBlank() {
		SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
		spr.sprite = Resources.Load<Sprite>("Sprites/BlankCard");
	}
}

public enum Suit {
		Spade, Heart, Diamond, Club
}