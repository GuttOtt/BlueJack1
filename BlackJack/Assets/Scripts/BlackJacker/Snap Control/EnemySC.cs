using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnapDecision { Snap, Fold, None }

public class EnemySC : MonoBehaviour, ISnapControl {
	[SerializeField] float minSnapConsiderTime = 1f;
	[SerializeField] float maxSnapConsiderTime = 2f;
	private bool isConsidering = false;
	private ISnapFoldAI ai;

	private void Awake() {
		TurnEventBus.Subscribe(TurnEventType.PLAYER_SNAP 
							, () => StartCoroutine(DecideReaction()) );
		ai = GetComponent<ISnapFoldAI>();
		if (ai == null)	
			ai = gameObject.AddComponent<BasicSnapFoldAI>();
	}

	public IEnumerator DecideSnap() {
		if (TurnManager.Instance.currentPhase != TurnManager.Instance.turn 
			&& TurnManager.Instance.currentPhase != TurnManager.Instance.stayed
			|| SnapManager.isEnemySnaped) {
			yield break;
		}

		isConsidering = true;

		yield return null;
		
		SnapDecision decision = ai.Decide();

		if (decision == SnapDecision.Snap) {
			Hand hand = GetComponent<Hand>();
			yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnRaise));
            TurnEventBus.Publish(TurnEventType.ENEMY_SNAP);
        }

		isConsidering = false;
	}

	public IEnumerator DecideReaction() {
		if (TurnManager.Instance.currentPhase != TurnManager.Instance.turn 
			&& TurnManager.Instance.currentPhase != TurnManager.Instance.stayed) {
			yield break;
		}

		isConsidering = true;
		Debug.Log("AI Considering");
		float considerTime = Random.Range(minSnapConsiderTime, maxSnapConsiderTime);
		yield return new WaitForSeconds(considerTime);
		
		SnapDecision decision = ai.Decide();

		if (decision == SnapDecision.Snap) {
			Hand hand = GetComponent<Hand>();
			yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnRaise));
			TurnEventBus.Publish(TurnEventType.ENEMY_SNAP);
		}
		else if (decision == SnapDecision.Fold) {
            Hand hand = GetComponent<Hand>();
            yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnFold));
            TurnEventBus.Publish(TurnEventType.ENEMY_FOLD);
		}

		isConsidering = false;
		Debug.Log("Considering Ends");
	}

	public bool IsConsidering() {
		return isConsidering;
	}
}

/*
	private IEnumerator SnapCR() {
		isConsidering = true;

		float considerTime = Random.Range(minSnapConsiderTime, maxSnapConsiderTime);

		yield return new WaitForSeconds(considerTime);

		bool isSnap = input.GetSnapInput();
		if (isSnap) {
			if (TurnManager.Instance.currentPhase == TurnManager.Instance.turn 
				|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)
				TurnEventBus.Publish(TurnEventType.ENEMY_SNAP);
		}

		isConsidering = false;
	}

	private IEnumerator FoldCR() {
		isConsidering = true;

		float considerTime = Random.Range(minFoldConsiderTime, maxFoldConsiderTime);

		yield return new WaitForSeconds(considerTime);

		bool isFold = input.GetFoldInput();
		if (isFold) {
			if (TurnManager.Instance.currentPhase == TurnManager.Instance.turn 
				|| TurnManager.Instance.currentPhase == TurnManager.Instance.stayed)
				TurnEventBus.Publish(TurnEventType.ENEMY_FOLD);
		}

		isConsidering = false;
	}
	*/