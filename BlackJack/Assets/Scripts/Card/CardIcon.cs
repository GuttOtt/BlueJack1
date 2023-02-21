using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectCondition {
	None, OnHit, OnEveryHit, OnWin, OnLose, OnFold, OnBurst, OnRaise, OnBlackJack, OnShowDown
}

public class CardIcon : MonoBehaviour {
	private Card card;
	public Gambler owner;
	private SpriteRenderer spr;
	private ICardEffect effect;
	[SerializeField] EffectCondition condition;

	private void Awake() {
		spr = GetComponent<SpriteRenderer>();
		effect = GetComponent<ICardEffect>();
	}

	public void TryToActivate(EffectCondition condition) {
		if (this.condition == condition) {
			effect.Activate();
		}
	}

	public void Initialize(Card card) {
		this.card = card;
		this.owner = card.owner;
	}
}