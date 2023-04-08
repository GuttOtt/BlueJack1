using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectSituation {
	None, OnOpen, OnEveryHit, OnWin, OnLose, OnFold, OnBurst,
	OnRaise, OnBlackJack, OnShowDown, OnDiscard, OnHiddenOpen
}

public class CardIcon : MonoBehaviour {
	private Card card;
	public Gambler owner;
	private SpriteRenderer spr;
	private ICardEffect effect;
	[SerializeField] EffectSituation situation;
	[SerializeField] GameObject activateAnimation;
	[SerializeField] private int id;
	public int ID { get => id; }

	private void Awake() {
		spr = GetComponent<SpriteRenderer>();
		effect = GetComponent<ICardEffect>();
	}

	public void TryToActivate(EffectSituation situation) {
		if (IsSatisfiedBy(situation)) {
			StartCoroutine(ActivateCoroutine());
		}
	}

	private IEnumerator ActivateCoroutine() {
		if (activateAnimation) ActivateAnimation();
		yield return new WaitForSeconds(1f);
		effect.Activate();
	}

	private void ActivateAnimation() {
		Instantiate(activateAnimation, transform);
	}

	public bool IsSatisfiedBy(EffectSituation situation) {
		if (this.situation == situation) return true;
		return false;
	}

	public void Initialize(Card card) {
		this.card = card;
		this.owner = card.owner;
	}
}