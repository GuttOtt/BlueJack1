using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardToChoose : MonoBehaviour {
	private CopyCardImage cardImage;
	private Text nameText, descText;
	private CardImageClicker clicker;

	private void Awake() {
		cardImage = gameObject.AddComponent<CopyCardImage>();
		nameText = transform.Find("Name Text BG").Find("Name Text").GetComponent<Text>();
		descText = transform.Find("Description Text BG").Find("Description Text").GetComponent<Text>();
		clicker = gameObject.AddComponent<CardImageClicker>();
	}

	public void SetUIs(CardData data) {
		cardImage.Draw(data);
		nameText.text = CardIconCSV.GetNameByID(data.ID);
		descText.text = CardIconCSV.GetDescByID(data.ID);
	}

	public void SetSelectAction(Action action) {
		clicker.onPointerClick += action;
	}

	public void ClearActions() {
		clicker.onPointerClick = null;
	}
}