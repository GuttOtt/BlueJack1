using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableCardImage : MonoBehaviour, IPointerClickHandler {
	private CopyCardImage cardImage;
	public CopyCardImage CardImage { get => cardImage; }
	public CardData Data { get => cardImage.GetData(); }
	
	public Action onClick = null;

	private void Awake() {
		cardImage = GetComponent<CopyCardImage>();

		if (!cardImage)
			cardImage = gameObject.AddComponent<CopyCardImage>();
	}

	public void Draw(CardData data) {
		cardImage.Draw(data);
	}

	public void OnPointerClick(PointerEventData eventData) {
		onClick();
	}
}
