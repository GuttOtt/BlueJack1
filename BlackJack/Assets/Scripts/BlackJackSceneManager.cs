using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackSceneManager : MonoBehaviour {
	[SerializeField] private BlackJacker player, opponent;
	[SerializeField] private TurnManager turnManager;
	[SerializeField] private int startHP;
	[SerializeField] private int ante;
	public static Vector2 cardSize;

    private void Awake() {

		
    }

    private void Start() {
		DeckSetting(player.GetComponent<Deck>(), true);
		DeckSetting(opponent.GetComponent<Deck>(), false);

		player.Heal(startHP);
		opponent.Heal(GameManager.currentEnemyData.hp.ToInt());

		SnapManager.ante = this.ante;

		turnManager.ToStartPhase();
	}

    public void DeckSetting(Deck deck, bool isPlayers) {
        List<CardData> deckData =
			isPlayers ? GameManager.playerDeck : GameManager.currentEnemyData.deckData;
        foreach (CardData data in deckData) {
            Card card = data.InstantiateAsCard();
            card.owner = deck.GetComponent<BlackJacker>();
            deck.AddCard(card);
        }
        deck.Shuffle();
    }

	public void EndBlackjackSceneByWin() {
		GameManager.ToDeckBuildingScene();
	}

	public void EndBlackjackSceneByLose() {
		Debug.Log("Game Over");
	}
}
