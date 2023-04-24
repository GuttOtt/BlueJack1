using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 각 씬의 DeckListDisplay 클래스들을 관리하는 역할
 * 모든 씬에 놓인 DeckListButton, BlackjackScene에 있는 DeckCover 등은 이 객체를 참조한다
 * 이 객체는 씬이 이동할 때마다 어떤 DeckListDisplay를 사용할지 결정한다
 */

public class DeckListDisplayManager : Singleton<DeckListDisplayManager> {
    private DeckListDisplay playerAllDeckDisplay;
    private DeckListDisplay blackjackSceneDeckDisplay;

    protected override void Awake() {
        base.Awake();

        if (!playerAllDeckDisplay) {
            playerAllDeckDisplay =
                Instantiate(Resources.Load<DeckListDisplay>("UIs/Player All Deck Display"));
            playerAllDeckDisplay.ClosePanel();
            DontDestroyOnLoad(playerAllDeckDisplay.gameObject);
        }

        if (!blackjackSceneDeckDisplay) {
            blackjackSceneDeckDisplay =
                Instantiate(Resources.Load<DeckListDisplay>("UIs/Blackjack Scene Deck Display"));
            blackjackSceneDeckDisplay.ClosePanel();
            DontDestroyOnLoad(blackjackSceneDeckDisplay.gameObject);
        }
    }

    public static void DrawPlayerAllDeckList() {
        Instance.playerAllDeckDisplay.DrawDeckList(GameManager.playerDeck);
    }

    public static void DrawDeckInBlackjackScene(List<CardData> deckData) {
        Instance.blackjackSceneDeckDisplay.DrawDeckList(deckData);
    }
}
