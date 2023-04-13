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
			yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnRaise));
            TurnEventBus.Publish(TurnEventType.PLAYER_SNAP);
			Debug.Log("PlayerSnaped " + Time.time);
        }
	}

	public IEnumerator DecideFold() {
		if (!(TurnManager.Instance.currentPhase == TurnManager.Instance.turn
			|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)) {
			yield break;
		}

        Hand hand = GetComponent<Hand>();
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnFold));
		TurnEventBus.Publish(TurnEventType.PLAYER_FOLD);
	}

	public bool IsConsidering() {
		return false;
	}
}
