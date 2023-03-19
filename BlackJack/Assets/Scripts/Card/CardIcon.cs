using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectCondition {
	None, OnHit, OnEveryHit, OnWin, OnLose, OnFold, OnBurst, OnRaise, OnBlackJack, OnShowDown, OnDiscard, OnOpen
}

public class CardIcon : MonoBehaviour {
	private Card card;
	public Gambler owner;
	private SpriteRenderer spr;
	private ICardEffect effect;
	[SerializeField] EffectCondition condition;
	[SerializeField] GameObject activateAnimation;

	private void Awake() {
		spr = GetComponent<SpriteRenderer>();
		effect = GetComponent<ICardEffect>();
	}

	public void TryToActivate(EffectCondition condition) {
		if (IsSatisfiedBy(condition)) {
			StartCoroutine(ActivateCoroutine());
		}
	}

	private IEnumerator ActivateCoroutine() {
		if (activateAnimation) ActivateAnimation();
		yield return new WaitForSeconds(1f);
		effect.Activate();
	}

	private void ActivateAnimation() {
		Debug.Log("Animation");
		Instantiate(activateAnimation, transform);
	}

	public bool IsSatisfiedBy(EffectCondition condition) {
		if (this.condition == condition) return true;
		return false;
	}

	public void Initialize(Card card) {
		this.card = card;
		this.owner = card.owner;
	}
}