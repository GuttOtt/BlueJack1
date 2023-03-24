using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyCardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] private Image numberImg, suitImg, iconImg;
	private CardData data;

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

	public void OnPointerEnter(PointerEventData eventData) {
		CardDescriptionManager.DrawDescription(data);
	}

	public void OnPointerExit(PointerEventData eventData) {
		CardDescriptionManager.ClosePanel();
	}
}