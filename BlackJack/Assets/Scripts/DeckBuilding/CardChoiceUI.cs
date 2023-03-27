using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoiceUI : MonoBehaviour {
	[SerializeField] private CardToChoose[] cards = new CardToChoose[3];
	[SerializeField] private GameObject panel;
	

	private void SetCardUIs(CardData[] dataArr) {
		for (int i = 0; i < 3; i++) {
			cards[i].ClearActions();
			cards[i].SetUIs(dataArr[i]);
		}
	}

	private void SetSelectActions(Action[] actions) {
		for (int i =0; i < 3; i++) {
			cards[i].SetSelectAction(actions[i]);
		}
	}

	public void OpenPanel(CardData[] dataArr, Action[] actions) {
		panel.SetActive(true);
		SetCardUIs(dataArr);
		SetSelectActions(actions);
	}

	public void ClosePanel() {
		panel.SetActive(false);
	}

}
