using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManipulation : MonoBehaviour, ICardManipulation {
    [SerializeField] Button[] numberButtons = new Button[7];
	private CardData data;

	private void Awake() {
		SetActiveAllButton(true);
		for (int i = 0; i < 7; i++) {
			Button button = numberButtons[i];
			int temp = i;

			button.onClick.AddListener( () => ChangeNumber(temp + 1) );
		}
		SetActiveAllButton(false);
	}

	private void ChangeNumber(int number) {
		data.ChangeNumber(number);
		GetComponent<DeckManipulationUI>().EndManipulation();
		SetActiveAllButton(false);
	}

	private void SetActiveAllButton(bool b) {
		foreach (Button button in numberButtons) {
			button.gameObject.SetActive(b);
		}
	}

	public void StartManipulation(CardData data) {
		this.data = data;
		SetActiveAllButton(true);
	}
}
