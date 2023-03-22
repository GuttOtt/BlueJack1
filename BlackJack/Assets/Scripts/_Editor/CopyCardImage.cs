using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyCardImage : MonoBehaviour {
	[SerializeField] private Image numberImg, suitImg, iconImg;
	private CardData data;

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