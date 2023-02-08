using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClient : MonoBehaviour {
	[SerializeField] private Deck playerDeck;
	[SerializeField] private Deck opponentDeck;
	[SerializeField] private CardGenerator cg;
	[SerializeField] private Wallet playerWallet;
	[SerializeField] private Wallet opponentWallet;
	[SerializeField] private RatioBettingAI bettingAI;
	[SerializeField] private BettingManager bettingManager;
	[SerializeField] private PlayerHit playerHit;
	[SerializeField] private RatioHitAI opponentHit;
	[SerializeField] private HitManager hitManager;

	private void Awake() {
		SetDefaultDeck(playerDeck);
		SetDefaultDeck(opponentDeck);

		//돈 관련 초기화
		playerWallet.Deposit(Money.wons(100));
		opponentWallet.Deposit(Money.wons(100));
		
		//턴 관련 초기화
		PlayerBettingUI pbu = FindObjectOfType<PlayerBettingUI>();
		RatioBettingAI obc = FindObjectOfType<RatioBettingAI>();
		bettingManager.SetBlackJackers(pbu, obc);


		//HitManager 관련 초기화
		hitManager.Initialize(playerHit, opponentHit, playerHit.GetComponent<Hand>(), opponentHit.GetComponent<Hand>());
	}


	private void Update() {
		if (Input.GetKeyDown(KeyCode.H)) {
			Debug.Log("Hit!");
			playerDeck.Hit();
		}
	}
	
	public void SetDefaultDeck(Deck deck) {
		for (int j = 0; j < 4; j++) {
			for (int i = 1; i <= 7; i++) {
				Card card = cg.CreateCard(i, (Suit)Random.Range(j, 4));
				deck.AddCard(card);
			}
		}
		deck.Shuffle();
	}
}
