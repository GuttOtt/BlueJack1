using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoices : MonoBehaviour {
	private CardChoiceUI ui;
	private CardData[] dataArr = new CardData[3];
	private Action[] selectActions = new Action[3];
	private Button button;

	public Action onSelect;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenChoiceUI);
	}

	private void OpenChoiceUI() {
		ui.OpenPanel(dataArr, selectActions);
	}

	public void Initialize(CardChoiceUI ui) {
		this.ui = ui;
		dataArr = new CardData[3];

		for (int i = 0; i < 3; i++) {
			dataArr[i] = CardGenerator.RandomCardData();
			int temp = i;
			Action act = () => Select(dataArr[temp]);
			selectActions[i] = act;
		}
	}

	private void Skip() {

	}

	private void Select(CardData data) {
		GameManager.AddToPlayerDeck(data);
		onSelect();
	}
}
