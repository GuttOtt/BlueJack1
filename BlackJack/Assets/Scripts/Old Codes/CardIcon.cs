using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIcon : MonoBehaviour {
	[SerializeField] private List<ICardEffect> effects = new List<ICardEffect>();

	public void OnHit() {
		foreach (ICardEffect effect in effects) {
			effect.Effect(EffectCondition.OnHit);
		}
	}
}
