using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate bool BetDele();

public class PlayerBetInput: MonoBehaviour, IBetInputSystem {
	[SerializeField] private Button callButton, raiseButton, foldButton;
	private List<Button> allButton;

	private void Awake() {
		allButton = new List<Button>() { callButton, raiseButton, foldButton };
		foreach (Button btn in allButton) {
			btn.gameObject.SetActive(false);
		}
	}

	public void SetInput(BetDele call, BetDele raise, BetDele fold) {
		callButton.onClick.AddListener(delegate {call();});
		raiseButton.onClick.AddListener(delegate {raise();});
		foldButton.onClick.AddListener(delegate {fold();});
	}

	public void StartGettingInput() {
		foreach (Button btn in allButton) {
			btn.gameObject.SetActive(true);
		}
	}

	public void EndGettingInput() {
		foreach (Button btn in allButton) {
			btn.gameObject.SetActive(false);
		}
	}
}