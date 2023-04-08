using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HitDecision { Hit, Stay, None };

public class PlayerTC : MonoBehaviour, ITurnControl {
	[SerializeField] Button nextButton;
	private BlackJacker blackjacker;
	private Hand hand;
	private IHitInput hitInput;
	private HitDecision hitDecision;
	private ISnapControl snapControl;
	private bool isStayed = false;
	private bool isNextPushed;
	public bool IsStayed { get => isStayed; }
	public bool IsBursted { get => blackjacker.IsBursted; }
	public int GetHandTotal { get => hand.GetTotal(); }
	
	private void Awake() {
		blackjacker = GetComponent<BlackJacker>();
		hand = GetComponent<Hand>();
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

	private IEnumerator ShowDownPhaseCR() {
		yield return StartCoroutine(hand.OpenHiddens());
		yield return StartCoroutine(hand.ActivateAllIcon(EffectSituation.OnShowDown));
		PublishEnd();
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

	public void ShowDownPhase() {
		StartCoroutine(ShowDownPhaseCR());
	}

	public IEnumerator BurstProcess() {
		hand.ShowHiddens();
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnBurst));
	}

	public IEnumerator FoldProcess() {
		yield return StartCoroutine(hand.ActivateAllHidden(EffectSituation.OnFold));
	}

	public IEnumerator WinProcess() {
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnWin));
	}

	public IEnumerator LoseProcess() {
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnLose));
		blackjacker.TakeDamage(SnapManager.pot);
	}

	public IEnumerator StayedProcess() {
		isNextPushed = false;
		yield return new WaitUntil(() => isNextPushed);
		PublishEnd();
	}

	public void TakeHit() { hitDecision = HitDecision.Hit; }
	public void TakeStay() { hitDecision = HitDecision.Stay; }

	public void Snap() {

	}

	public void Fold() {

	}
}
