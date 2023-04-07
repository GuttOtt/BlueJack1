using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HitDecision { Hit, Stay, None };

public class PlayerTC : MonoBehaviour, ITurnControl {
	private BlackJacker blackjacker;
	private IHitInput hitInput;
	private bool isStayed = false;
	private HitDecision hitDecision;
	private ISnapControl snapControl;
	public bool IsStayed { get => isStayed; }
	public bool IsBursted { get => blackjacker.IsBursted; }
	
	private void Awake() {
		blackjacker = GetComponent<BlackJacker>();
		hitInput = GetComponent<IHitInput>();
		snapControl = GetComponent<ISnapControl>();
		hitDecision = HitDecision.None;
	}

	private IEnumerator StartPhaseCR() {
		blackjacker.StartSetting();
		isStayed = false;

		PublishEnd();

		yield return null;
	}

	private IEnumerator TurnPhaseCR() {
		if (isStayed) {
			PublishEnd();
			yield break;
		}

		yield return StartCoroutine(hitInput.GetInput(this));
		yield return new WaitWhile( () => snapControl.IsConsidering() );

		if (hitDecision == HitDecision.Stay) {
			isStayed = true;
		}
		else if (hitDecision == HitDecision.Hit) {
			isStayed = false;
		}

		PublishEnd();
	}

	private IEnumerator IntervalPhaseCR() {
		if (!isStayed) {
			blackjacker.Hit();
		}
		else {
			//Stay 그래픽 처리
		}

		PublishEnd();
		
		yield return null;
	}

	private void PublishEnd() {
		if (gameObject.CompareTag("Player")) {
			TurnEventBus.Publish(TurnEventType.PLAYER_END);
		}
		else if (gameObject.CompareTag("Opponent")) {
			TurnEventBus.Publish(TurnEventType.ENEMY_END);
		}
	}

	public void StartPhase() {
		StartCoroutine(StartPhaseCR());
	}

	public void TurnPhase() {
		StartCoroutine(TurnPhaseCR());
	}
	
	public void IntervalPhase() {
		StartCoroutine(IntervalPhaseCR());
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
