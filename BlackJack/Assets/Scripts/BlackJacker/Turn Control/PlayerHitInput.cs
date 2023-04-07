using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitInput : MonoBehaviour, IHitInput {
	[SerializeField] public GameObject hitDecisionButtons;
	[SerializeField] private Button hitButton, stayButton;
	private HitDecision hitDecision = HitDecision.None;

	private void Awake() {
		hitButton.onClick.AddListener( () => hitDecision = HitDecision.Hit);
		stayButton.onClick.AddListener( () => hitDecision = HitDecision.Stay);
		hitDecisionButtons.SetActive(false);
	}

	public IEnumerator GetInput(ITurnControl turnControl) {
		hitDecisionButtons.SetActive(true);

		yield return new WaitUntil(() => hitDecision != HitDecision.None);

		if (hitDecision == HitDecision.Hit) {
			turnControl.TakeHit();
		}
		else if (hitDecision == HitDecision.Stay) {
			turnControl.TakeStay();
		}

		hitDecision = HitDecision.None;

		hitDecisionButtons.SetActive(false);
	}
}
