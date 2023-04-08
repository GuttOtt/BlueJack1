using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapManager : MonoBehaviour {
	[SerializeField] Text potText;
	public static int pot = 1;
	public static bool isPlayerSnaped = false;
	public static bool isEnemySnaped = false;

	private void Awake() {
		TurnEventBus.Subscribe(TurnEventType.PLAYER_SNAP, () => TakePlayerSnap());
		TurnEventBus.Subscribe(TurnEventType.ENEMY_SNAP, () => TakeEnemySnap());
		potText.text = pot.ToString();
	}

	private void TakePlayerSnap() {
		RaisePot();
		isPlayerSnaped = true;
	}

	private void TakeEnemySnap() {
		RaisePot();
		isEnemySnaped = true;
	}

	private void RaisePot() {
		pot *= 2;
		potText.text = pot.ToString();
	}
}
