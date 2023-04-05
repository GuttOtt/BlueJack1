using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnControl {
	void StartPhase();
	void TurnPhase();
	void IntervalPhase();
	void EndPhase();
	void Fold();
	void Snap();
}
