using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackSceneManager : Singleton<BlackJackSceneManager> {
	[SerializeField] private BlackJacker player, opponent;
	[SerializeField] private TurnManager turnManager;
	[SerializeField] private int startHP;
	[SerializeField] private int ante;
	public static Vector2 cardSize;

    protected override void Awake() {
		base.Awake();

		Sprite cardSprite = Resources.Load<Sprite>("Sprites/BlankCard");
		cardSize = cardSprite.bounds.size;
    }

    private void Start() {
		SandboxManager.Instance.DeckSetting(player.GetComponent<Deck>(), true);
		SandboxManager.Instance.DeckSetting(opponent.GetComponent<Deck>(), false);

		player.Heal(startHP);
		opponent.Heal(startHP);

		SnapManager.ante = this.ante;

		turnManager.ToStartPhase();
	}
}
