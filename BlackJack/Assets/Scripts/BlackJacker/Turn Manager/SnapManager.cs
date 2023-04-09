using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapManager : MonoBehaviour {
	[SerializeField] Text potText;
	[SerializeField] Image tempPotImg;
	[SerializeField] Image potImg;
	public static int defaultPot = 5;
	public static int tempPot = 5;
	public static int pot = 5;
	public static float potSize = 1;
	public static bool isPlayerSnaped = false;
	public static bool isEnemySnaped = false;

	private void Awake() {
		TurnEventBus.Subscribe(TurnEventType.PLAYER_SNAP, () => TakePlayerSnap());
		TurnEventBus.Subscribe(TurnEventType.ENEMY_SNAP, () => TakeEnemySnap());
		TurnEventBus.Subscribe(TurnEventType.NEW_ROUND, ResetPot);
		TurnEventBus.Subscribe(TurnEventType.TURN_END, RaisePot);
		TurnEventBus.Subscribe(TurnEventType.VICTORY_PHASE
							,() => { RaiseTempPot(); RaisePot(); });

		potText.text = pot.ToString();
	}

	private void TakePlayerSnap() {
		RaiseTempPot();
		isPlayerSnaped = true;
	}

	private void TakeEnemySnap() {
		RaiseTempPot();
        isEnemySnaped = true;
	}

	private void RaiseTempPot() {
		tempPot *= 2;
		potSize += 0.7f;
		ResizePotImage(tempPotImg);
    }
    private void RaisePot() {
        pot = tempPot;
        ResizePotImage(potImg);
		potText.text = pot.ToString();
    }

    private void ResetPot() {
		pot = defaultPot;
		tempPot = pot;
		potSize = 1;
		isPlayerSnaped = false;
		isEnemySnaped = false;
		potText.text = pot.ToString();

		ResizePotImage(tempPotImg);
		ResizePotImage(potImg);
	}


	private void ResizePotImage(Image img) {
        RectTransform rect = img.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(50f * potSize, 50f * potSize);
    }
}
