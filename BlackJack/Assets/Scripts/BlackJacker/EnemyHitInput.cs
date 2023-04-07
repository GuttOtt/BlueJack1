using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitInput : MonoBehaviour, IHitInput {
	private IHitAI ai;	

	private void Awake() {
		ai = GetComponent<IHitAI>();
		if (ai == null) {
			ai = gameObject.AddComponent<BasicHitAI>();
		}
	}

	public IEnumerator GetInput(ITurnControl turnControl) {
		HitDecision hitDecision = ai.DecideHit();
		if (hitDecision == HitDecision.Hit) {
			turnControl.TakeHit();
		}
		else if (hitDecision == HitDecision.Stay) {
			turnControl.TakeStay();
		}
		yield return null;
	}
}