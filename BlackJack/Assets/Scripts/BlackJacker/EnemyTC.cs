using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTC : MonoBehaviour, ITurnControl {
	private BlackJacker blackjacker;
	private bool isStayed = false;
	private bool isConsidering = false;
	private HitDecision hitDecision;
	public bool IsStayed { get => isStayed; }
	public bool IsBursted { get => blackjacker.IsBursted; }

	private IEnumerator StartPhaseCR() {
		blackjacker.StartSetting();
		isStayed = false;
		hitDecision = HitDecision.None;

		TurnEventBus.Publish(TurnEventType.PLAYER_END);

		yield return null;
	}

	private IEnumerator TurnPhaseCR() {
		if (isStayed) {
			TurnEventBus.Publish(TurnEventType.PLAYER_END);
			yield break;
		}

		//hitDecisionButtons.SetActive(true);

		yield return new WaitUntil( () => hitDecision != HitDecision.None);
		if (hitDecision == HitDecision.Stay) {
			isStayed = true;
		}
		else if (hitDecision == HitDecision.Hit) {
			isStayed = false;
		}

		//hitDecisionButtons.SetActive(false);

		TurnEventBus.Publish(TurnEventType.PLAYER_END);
	}

	public void StartPhase() {
	}

	public void TurnPhase() {
		TurnEventBus.Publish(TurnEventType.ENEMY_END);
	}

	public void IntervalPhase() {

	}

	public void EndPhase() {

	}

	public void Fold() {

	}

	public void Snap() {

	}
}
