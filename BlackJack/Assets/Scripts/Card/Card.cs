using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card: MonoBehaviour {
	public Gambler owner;
	private CardIcon icon;
	private CardIcon iconPrefab;
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
				isFront = true;
			}
			else {
				DrawBack();
				isFront = false;
			}
		}
	}
	private Vector3 movePosition = Vector3.zero;
	private float moveSpeed = 0;
	public int IconID { get => icon.ID; }
	public string Name {
		get {
			return CardIconCSV.GetNameByID(iconPrefab.ID);
		}
	}
	public string Desc {
		get {
			return CardIconCSV.GetDescByID(iconPrefab.ID);
		}
	}

	private void Awake() {
		BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
		col.size = new Vector2(2f, 110/40f);
	}

	private void Update() {
		if (movePosition != null) {
			transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	private void OnMouseEnter() {
		if (!isFront || DeckListUI.IsTurnedOn) return;
		CardDescriptionManager.DrawDescription(GetData());
	}

	private void OnMouseExit() {
		if (DeckListUI.IsTurnedOn) return;
		CardDescriptionManager.ClosePanel();
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
		sprite = CardSpriteContainer.NumberToSprite(number);

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
		sprite = CardSpriteContainer.SuitToSprite(suit);
		
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

		this.iconPrefab = iconPrefab;
		icon = Instantiate(iconPrefab, this.transform);
		icon.transform.localPosition = Vector3.zero + Vector3.back * 0.1f;
		icon.Initialize(this);
	}

	public bool ActivateIcon(EffectSituation situation) {
		if (!icon.IsSatisfiedBy(situation))
			return false;
		icon.TryToActivate(situation);
		return true;
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

	public CardData GetData() {
		return new CardData(number, suit, iconPrefab);
	}
}

public enum Suit {
		Spade, Heart, Diamond, Club
}