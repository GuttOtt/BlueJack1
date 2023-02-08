using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioHitAI : MonoBehaviour, IHitClient {
	[SerializeField] private Deck deck;	
    [SerializeField] private Hand hand;
    private bool isStayed;

    private void Awake() {
        hand = GetComponent<Hand>();
        deck = GetComponent<Deck>();
    }

    public void StartTurn() {

    }

    private IEnumerator HitDelay() {
        yield return new WaitForSeconds(2f);
    }

    public bool DecideHit() {
        if (isStayed)
            return true;

        if (hand.GetTotal() > 18) {
            isStayed = true;
        }
        else {
            deck.Hit();
        }

        return true;
    }

    public void EndTurn() {

    }

    public void StartSetting() {
        deck.Hit();
        deck.Hit();
        isStayed = false;
    }
}
