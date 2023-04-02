using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitManipulation : MonoBehaviour, ICardManipulation {
	[SerializeField] Button[] suitButtons = new Button[4];
	private CardData data;

	private void Awake() {
		SetActiveAllButton(true);
		for (int i = 0; i < 4; i++) {
			Button button = suitButtons[i];
			int temp = i;

			button.onClick.AddListener( () => ChangeSuit((Suit) temp) );
		}
		SetActiveAllButton(false);
	}

	private void ChangeSuit(Suit suit) {
		data.ChangeSuit(suit);
		GetComponent<DeckManipulationUI>().EndManipulation();
		SetActiveAllButton(false);
	}

	private void SetActiveAllButton(bool b) {
		foreach (Button button in suitButtons) {
			button.gameObject.SetActive(b);
		}
	}

	public void StartManipulation(CardData data) {
		Debug.Log("StartManipulation");
		this.data = data;
		SetActiveAllButton(true);
	}
}
