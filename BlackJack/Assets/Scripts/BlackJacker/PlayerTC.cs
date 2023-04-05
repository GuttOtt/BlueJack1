using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HitDecision { Hit, Stay, None };

public class PlayerTC : MonoBehaviour, ITurnControl {
	[SerializeField] private Button hitButton, stayButton;
	[SerializeField] private GameObject hitDecisionButtons;
	private BlackJacker blackjacker;
	private HitDecision hitDecision = HitDecision.None;
	private bool isStayed = false;
	public bool IsStayed { get => isStayed; }
	public bool IsBursted { get => blackjacker.IsBursted; }
	
	private void Awake() {
		blackjacker = GetComponent<BlackJacker>();
	}

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

		hitDecisionButtons.SetActive(true);

		yield return new WaitUntil( () => hitDecision != HitDecision.None);
		if (hitDecision == HitDecision.Stay) {
			isStayed = true;
		}
		else if (hitDecision == HitDecision.Hit) {
			isStayed = false;
		}

		hitDecisionButtons.SetActive(false);

		TurnEventBus.Publish(TurnEventType.PLAYER_END);
	}

	private IEnumerator IntervalPhaseCR() {
		yield return null;
	}

	public void StartPhase() {
		StartCoroutine(StartPhaseCR());
	}

	public void TurnPhase() {
		StartCoroutine(TurnPhaseCR());
	}
	
	public void IntervalPhase() {

	}

	public void EndPhase() {

	}

	public void TakeHit() { hitDecision = HitDecision.Hit; }
	public void TakeStay() { hitDecision = HitDecision.Stay; }

	public void Snap() {

	}

	public void Fold() {

	}
}
