using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectSituation {
	None, OnOpen, OnEveryHit, OnWin, OnLose, OnFold, OnBurst,
	OnRaise, OnBlackJack, OnShowDown, OnDiscard, OnHiddenOpen
}

public class CardIcon : MonoBehaviour {
	private SpriteRenderer spr;
	private ICardEffect effect;
	[SerializeField] EffectSituation situation;
	[SerializeField] GameObject activateAnimation;
	[SerializeField] private int id;
	public int ID { get => id; }
    public BlackJacker Owner { 
		get => transform.parent.GetComponent<Card>().owner;
	}

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

}