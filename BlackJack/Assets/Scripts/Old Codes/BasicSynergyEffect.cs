using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSynergyEffect: MonoBehaviour, ICardEffect {
	private Hand hand;
	private ICardSynergy synergy;
	private EffectCondition condition;

	private void Awake() {
		condition = EffectCondition.OnHit;
	}

	public void Effect(EffectCondition condition) {
		if (this.condition != condition)
			return;

		int numberOfSynergy = hand.NumberOfSynergy(synergy);
		Debug.Log(numberOfSynergy);
	}
}