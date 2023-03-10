using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card: MonoBehaviour {
	public Gambler owner;
	private CardIcon icon;
	private int number;
	private Suit suit;
	private GameObject numberObj;
	private GameObject suitObj;
	private SpriteRenderer spriteRenderer;
	private bool isFront = false;
	public bool IsFront {
		get => isFront;
		set {
			if (value) {
				DrawFront();
			}
			else {
				DrawBack();
			}
		}
	}
	private Vector3 movePosition = Vector3.zero;
	private float moveSpeed = 0;

	private void Update() {
		if (movePosition != null) {
			transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed);
		}
	}

	public void Initialize(int number, Suit suit) {
		this.number = number;
		this.suit = suit;
		numberObj = new GameObject("CardNumber");
		suitObj = new GameObject("CardSuit");
		spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
		MakeNumberObj();
		MakeSuitObj();
		DrawBack();
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

	public Suit GetSuit() {
		return suit;
	}

	private void MakeNumberObj() {
		if (numberObj) {
			Destroy(numberObj);
		}
		numberObj = new GameObject("CardNumber");
		
		Sprite sprite = null;
		switch (number) {
			case 0:
				sprite = Resources.Load<Sprite>("Sprites/CardNumber0");
				break;
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

		SpriteRenderer spr = numberObj.AddComponent<SpriteRenderer>();
		spr.sprite = sprite;
		if (suit == Suit.Spade || suit == Suit.Club)
			spr.color = Color.black;

		numberObj.transform.SetParent(this.transform);
		numberObj.transform.localPosition = Vector3.zero + Vector3.back * 0.1f;
	}

	private void MakeSuitObj() {
		if (suitObj) {
			Destroy(suitObj);
		}
		suitObj = new GameObject("CardSuit");

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
		
		SpriteRenderer spr = suitObj.AddComponent<SpriteRenderer>();
		spr.sprite = sprite;

		suitObj.transform.SetParent(this.transform);
		suitObj.transform.localPosition = Vector3.zero + Vector3.back * 0.1f;
	}

	private void DrawFront() {
		spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/BlankCard");
		numberObj.SetActive(true);
		suitObj.SetActive(true);
		icon.gameObject.SetActive(true);
	}

	private void DrawBack() {
		spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/CardBack");
		numberObj.SetActive(false);
		suitObj.SetActive(false);
		icon.gameObject.SetActive(false);
	}

	public void MoveTo(Vector3 pos) {
		movePosition = pos;
		moveSpeed = (pos - transform.position).magnitude * Time.deltaTime * 3f;
	}

	public void AddIcon(CardIcon iconPrefab) {
		if (icon != null) {
			Destroy(icon);
		}

		icon = Instantiate(iconPrefab, this.transform);
		icon.transform.localPosition = Vector3.zero + Vector3.back * 0.1f;
		icon.Initialize(this);
	}

	public void ActivateIcon(EffectCondition condition) {
		icon.TryToActivate(condition);
	}

	public void ChangeSuit(Suit suit) {
		this.suit = suit;
		MakeSuitObj();
		MakeNumberObj();
	}

	public void ChangeNumber(int number) {
		this.number = number;
		MakeNumberObj();
	}
}

public enum Suit {
		Spade, Heart, Diamond, Club
}