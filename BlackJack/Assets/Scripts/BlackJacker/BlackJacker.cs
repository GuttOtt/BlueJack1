using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJacker : MonoBehaviour {
	[SerializeField] private HPGraphic hpGraphic;
	[SerializeField] private ArmourGraphic armourGraphic;
	[SerializeField] private HP hp;
    [SerializeField] private Armour armour;
    private Hand hand;
	private Deck deck;
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
		hp = HP.Zero;
		armour = Armour.Zero;
		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
		hpGraphic.SetHP(hp);
		armourGraphic.SetArmour(armour);

		opponent = gameObject.CompareTag("Player")
			? GameObject.FindWithTag("Opponent").GetComponent<BlackJacker>()
			: GameObject.FindWithTag("Player").GetComponent<BlackJacker>();
	}

	public IEnumerator StartSetting() {
        ResetArmour();
        DamageDealtBeforeShowdown = 0;

        hand.DiscardAll();
		yield return new WaitForSeconds(1f);
		deck.ClosedHit();
		deck.ClosedHit();
	}

	public void Hit() {
		deck.OpenHit();
	}

	public void TakeDamage(int damage) {
		int amount = armour.Loss(damage);
		hp.TakeDamage(amount);
	}

	public void DealDamage( int damage) {
		opponent.TakeDamage(damage);
        DamageDealtBeforeShowdown += damage;
	}

	public void Heal(int amount) {
		hp.Heal(amount);
	}

	public void GainArmour(int amount) {
		armour.Gain(amount);
	}

	public void ResetArmour() {
		armour.Reset();
	}
}
