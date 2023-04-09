using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurnEventType {
	PLAYER_END, ENEMY_END, PLAYER_SNAP, ENEMY_SNAP, PLAYER_FOLD, ENEMY_FOLD
	, NEW_ROUND, TURN_END, VICTORY_PHASE
}

public class TurnEventBus {
	private static readonly IDictionary<TurnEventType, UnityEvent> Events
							= new Dictionary<TurnEventType, UnityEvent>();

	public static void Subscribe(TurnEventType eventType, UnityAction listener) {
		UnityEvent thisEvent;

		if (Events.TryGetValue(eventType, out thisEvent)) {
			thisEvent.AddListener(listener);
			return;
		}

		thisEvent = new UnityEvent();
		thisEvent.AddListener(listener);
		Events.Add(eventType, thisEvent);
	}

	public static void Unsubscribe(TurnEventType eventType, UnityAction listener) {
		UnityEvent thisEvent;

		if (Events.TryGetValue(eventType, out thisEvent)) {
			thisEvent.RemoveListener(listener);
		}
	}

	public static void Publish(TurnEventType eventType) {
		UnityEvent thisEvent;

		if (Events.TryGetValue(eventType, out thisEvent)) {
			thisEvent.Invoke();
		}
	}

	//Clear Method
}
