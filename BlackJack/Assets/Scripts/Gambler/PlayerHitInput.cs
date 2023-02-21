using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitInput : MonoBehaviour, IHitInputSystem{
	[SerializeField] private Button hitButton, stayButton;
	private List<Button> allButton;

	private void Awake() {
		allButton = new List<Button>() { hitButton, stayButton };
		foreach (Button btn in allButton) {
			btn.gameObject.SetActive(false);
		}
	}

	public void SetInput(HitDele hit, HitDele stay) {
		hitButton.onClick.AddListener(delegate {hit();});
		stayButton.onClick.AddListener(delegate {stay();});
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
