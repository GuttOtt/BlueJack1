using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSC : MonoBehaviour, ISnapControl {
	[SerializeField] private Button snapButton, foldButton; // Buttonized·Î ¹Ù²Ü °Í

	private void Awake() {
		snapButton.onClick.AddListener(() => StartCoroutine(DecideSnap()));
		foldButton.onClick.AddListener(() => StartCoroutine(DecideFold()));
	}

	public IEnumerator DecideSnap() {
		if (!(TurnManager.Instance.currentPhase == TurnManager.Instance.turn
			|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)) {
			yield break;
		}
		if (!SnapManager.isPlayerSnaped) {
			Hand hand = GetComponent<Hand>();
            TurnEventBus.Publish(TurnEventType.PLAYER_SNAP);
            yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnRaise));
        }
	}

	public IEnumerator DecideFold() {
		if (!(TurnManager.Instance.currentPhase == TurnManager.Instance.turn
			|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)) {
			yield break;
		}

		yield return StartCoroutine(GetComponent<BlackJacker>().FoldCR());
		TurnEventBus.Publish(TurnEventType.PLAYER_FOLD);
	}

	public bool IsConsidering() {
		return false;
	}
}
