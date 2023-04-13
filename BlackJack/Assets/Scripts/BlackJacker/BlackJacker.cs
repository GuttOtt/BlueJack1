using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJacker : MonoBehaviour {
	[SerializeField] private HPGraphic hpGraphic;
	[SerializeField] private HP hp;
	private Hand hand;
	private Deck deck;
	private int armour = 0;
	private int _damageDealtBeforeShowdown;
	public int DamageDealtBeforeShowdown {
		get => _damageDealtBeforeShowdown;
		set {
			if (TurnManager.Instance.currentPhase == TurnManager.Instance.showdown) {
				return;
			}
			else {
				_damageDealtBeforeShowdown = value;
			}
		}
	}
	public BlackJacker opponent;

	private void Awake() {
		hp = new HP(0);
		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
		hpGraphic.SetHP(hp);
		TurnEventBus.Subscribe(TurnEventType.NEW_ROUND, NewRoundInitialize);

		opponent = gameObject.CompareTag("Player")
			? GameObject.FindWithTag("Opponent").GetComponent<BlackJacker>()
			: GameObject.FindWithTag("Player").GetComponent<BlackJacker>();
	}

	public IEnumerator StartSetting() {
		hand.DiscardAll();
		yield return new WaitForSeconds(1.5f);
		deck.ClosedHit();
		deck.ClosedHit();
		deck.OpenHit();
	}

	public void Hit() {
		deck.OpenHit();
	}

	public void TakeDamage(int damage) {
		int amount = damage - armour;
		hp.TakeDamage(amount);
		Debug.Log("TakeDamage");
	}

	public void DealDamage( int damage) {
		opponent.TakeDamage(damage);
		Debug.Log("DealDamage");
	}

	public void Heal(int amount) {
		hp.Heal(amount);
	}

	public void GainArmour(int amount) {
		armour += amount;
	}

	public void ResetArmour() {
		armour = 0;
	}

	public void NewRoundInitialize() {
		ResetArmour();
		DamageDealtBeforeShowdown = 0;
	}
}
