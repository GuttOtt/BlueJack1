using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawing : MonoBehaviour {
	private GameObject numberObj, suitObj;

	public void Draw(int number, Suit suit) {
		MakeNumberObj(number, suit);
		MakeSuitObj(suit);
	}

	public void MakeNumberObj(int number, Suit suit) {
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

	private void MakeSuitObj(Suit suit) {
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

}
