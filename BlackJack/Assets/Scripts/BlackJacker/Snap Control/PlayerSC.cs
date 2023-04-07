using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSC : MonoBehaviour, ISnapControl {
	[SerializeField] private Button snapButton, foldButton; // Buttonized·Î ¹Ù²Ü °Í

	private void Awake() {
		snapButton.onClick.AddListener(DecideSnap);
		foldButton.onClick.AddListener(DecideFold);
	}

	public void DecideSnap() {
		if (!(TurnManager.Instance.currentPhase == TurnManager.Instance.turn
			|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)) {
			return;
		}
		if (!SnapManager.isPlayerSnaped) {
			TurnEventBus.Publish(TurnEventType.PLAYER_SNAP);
		}
	}

	public void DecideFold() {
		if (!(TurnManager.Instance.currentPhase == TurnManager.Instance.turn
			|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)) {
			return;
		}
		TurnEventBus.Publish(TurnEventType.PLAYER_FOLD);
	}

	public bool IsConsidering() {
		return false;
	}
}
