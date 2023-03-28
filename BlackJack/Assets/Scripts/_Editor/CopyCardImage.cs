using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyCardImage : MonoBehaviour {
	[SerializeField] private Image numberImg, suitImg, iconImg;
	private CardData data;

	private void Awake() {
		Image img = GetComponent<Image>();
		if (!img)
			img = gameObject.AddComponent<Image>();

		img.sprite = Resources.Load<Sprite>("Sprites/BlankCard");

		if (!numberImg) {
			numberImg = CreateSubImage("Number Image");
		}
		if (!suitImg) {
			suitImg = CreateSubImage("Suit Image");
		}
		if (!iconImg) {
			iconImg = CreateSubImage("Icon Image");
		}
	}

	private Image CreateSubImage(string name) {
		Rect rect = GetComponent<RectTransform>().rect;
		Vector2 size = new Vector2(rect.width, rect.height);
		GameObject obj = new GameObject(name);
		RectTransform objRect = obj.AddComponent<RectTransform>();
		objRect.SetParent(this.transform);
		objRect.sizeDelta = size;
		objRect.localPosition = Vector2.zero;
		
		return obj.AddComponent<Image>();
	}

	private void OnMouseEnter() {
		Debug.Log("OnMouseEnter");
		CardDescriptionManager.DrawDescription(data);
	}

	public void Draw(CardData data) {
		this.data = data;
		numberImg.sprite = CardSpriteContainer.NumberToSprite(data.number);
		suitImg.sprite = CardSpriteContainer.SuitToSprite(data.suit);
		iconImg.sprite = data.iconPrefab.GetComponent<SpriteRenderer>().sprite;

		numberImg.color = data.suit == Suit.Spade || data.suit == Suit.Club ? Color.black : Color.red;
	}
	
	public CardData GetData() {
		return data;
	}
}