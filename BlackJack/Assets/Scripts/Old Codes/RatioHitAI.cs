using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioHitAI : MonoBehaviour, IHitClient {
	[SerializeField] private Deck deck;	
    [SerializeField] private Hand hand;
    [SerializeField] private OpponentDialogue dialogue;
    private bool isStayed;

    private void Awake() {
        hand = GetComponent<Hand>();
        deck = GetComponent<Deck>();
        isStayed = false;
    }

    public void StartTurn() {

    }

    private IEnumerator HitDelay() {
        yield return new WaitForSeconds(2f);
    }

    public bool DecideHit() {
        if (isStayed){
            dialogue.StayText();
            return true;
        }

        if (hand.GetTotal() > 18) {
            dialogue.StayText();
            isStayed = true;
        }
        else {
            dialogue.HitText();
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
