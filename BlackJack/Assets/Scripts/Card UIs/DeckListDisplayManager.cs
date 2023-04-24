using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * �� ���� DeckListDisplay Ŭ�������� �����ϴ� ����
 * ��� ���� ���� DeckListButton, BlackjackScene�� �ִ� DeckCover ���� �� ��ü�� �����Ѵ�
 * �� ��ü�� ���� �̵��� ������ � DeckListDisplay�� ������� �����Ѵ�
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
