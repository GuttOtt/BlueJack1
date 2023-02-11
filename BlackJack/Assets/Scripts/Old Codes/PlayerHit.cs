using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHitClient {
	void StartTurn();
	void EndTurn();
	bool DecideHit();
	void StartSetting();
}

public class PlayerHit : MonoBehaviour, IHitClient {
	[SerializeField] private Deck deck;	
	[SerializeField] private Button hitButton;
	[SerializeField] private Button stayButton;
	private List<Button> allButton;

	private enum HitAction {
		Hit, Stay, None
	}
	private HitAction input = HitAction.None;

	private bool isStayed = false;

	private void Awake() {
		allButton = new List<Button>() { hitButton, stayButton };
		hitButton.onClick.AddListener(delegate {input = HitAction.Hit;});
		stayButton.onClick.AddListener(delegate {input = HitAction.Stay;});
	}

	public void StartTurn() {
		if (!isStayed) {
			DrawButtons();
		}
		else {
			input = HitAction.Stay;
		}
	}

	public bool DecideHit() {
		switch (input) {
			case HitAction.Hit:
				deck.Hit();
				return true;
			case HitAction.Stay:
				isStayed = true;
				return true;
		}
		return false;
	}

	public void EndTurn() {
		input = HitAction.None;
		EraseButtons();
	}

	private void DrawButtons() {
		foreach (Button button in allButton) {
			button.gameObject.SetActive(true);
		}
	}

	private void EraseButtons() {
		foreach (Button button in allButton) {
			button.gameObject.SetActive(false);
		}
	}

	public void StartSetting() {
		deck.Hit();
		deck.Hit();
		isStayed = false;
	}
}