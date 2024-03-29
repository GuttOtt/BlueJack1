using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquireCardManager : MonoBehaviour {
	[SerializeField] CardChoices cardChoicesPrefab;
	[SerializeField] CardChoiceUI cardChoiceUI;
	[SerializeField] private List<CardChoices> choicesList = new List<CardChoices>();
	private GameObject panel;
	private DeckBuildingSceneManager deckBuildingSceneManager;
	
	private void Awake() {
		cardChoiceUI = transform.Find("Card Choice UI").GetComponent<CardChoiceUI>();
		cardChoiceUI.ClosePanel();
		panel = transform.Find("Panel").gameObject;
		deckBuildingSceneManager = FindObjectOfType<DeckBuildingSceneManager>();
	}

	public void StartDeckBuilding(int number) {
		panel.SetActive(true);
		for (int i = 0; i < number; i++) {
			AddCardChoices();
		}
		Arrange();
	}

	private void AddCardChoices() {
		CardChoices choices = Instantiate(cardChoicesPrefab, panel.transform);
		choices.Initialize(cardChoiceUI);
		choicesList.Add(choices);
		choices.onSelect += () => DeleteCardChoices(choices);
	}

	private void Arrange() {
		float panelHeight = panel.GetComponent<RectTransform>().rect.height;
		float buttonHeight = cardChoicesPrefab.GetComponent<RectTransform>().rect.height;
		float yOrigin = buttonHeight / 2f;

		for (int i = 0; i < choicesList.Count; i++) {
			RectTransform rect = choicesList[i].GetComponent<RectTransform>();
			rect.localPosition = new Vector2(0, -(yOrigin + buttonHeight * i));
		}
	}

	public void DeleteCardChoices(CardChoices choices) {
		choicesList.Remove(choices);
		Destroy(choices.gameObject);
		cardChoiceUI.ClosePanel();
		Arrange();
	}

	public void EndDeckBuilding() {
		for (int i = choicesList.Count - 1; i >= 0; i--) {
			DeleteCardChoices(choicesList[i]);
		}

		deckBuildingSceneManager.StartDeckManipulation();

		gameObject.SetActive(false);
	}
}
