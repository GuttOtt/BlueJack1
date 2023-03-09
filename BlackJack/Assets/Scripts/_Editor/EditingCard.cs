using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingCard : MonoBehaviour {
	private CardIcon icon;
	private CardIcon iconPrefab;
	private int number;
	private Suit suit;
	private GameObject numberObj;
	private GameObject suitObj;
	private CardDrawing drawing;
	
	public void Initialize(int number, Suit suit) {
		SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
		spr.sprite = Resources.Load<Sprite>("Sprites/BlankCard");
		gameObject.AddComponent<BoxCollider2D>();
		drawing = gameObject.AddComponent<CardDrawing>() as CardDrawing;
		ChangeNumber(number);
		ChangeSuit(suit);
		ChangeIcon(Resources.Load<CardIcon>("CardIcons/None Icon"));
	}

	public void Initialize(int number, Suit suit, CardIcon iconPrefab) {
		Initialize(number, suit);
		ChangeIcon(iconPrefab);
	}

	public void ChangeSuit(Suit suit) {
		this.suit = suit;
		drawing.Draw(number, suit);
	}

	public void ChangeNumber(int number) {
		Debug.Log(number);
		this.number = number;
		drawing.Draw(number, suit);
	}

	public void ChangeIcon(CardIcon iconPrefab) {
		this.iconPrefab = iconPrefab;

		if (icon != null) {
			Destroy(icon.gameObject);
		}

		icon = Instantiate(iconPrefab, this.transform);
		icon.transform.localPosition = Vector3.zero + Vector3.back * 0.1f;
	}

	public CardData GetData() {
		return new CardData(number, suit, iconPrefab);
	}
}
