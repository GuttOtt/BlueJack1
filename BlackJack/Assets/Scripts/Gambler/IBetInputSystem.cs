using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBetInputSystem {
	void SetInput(BetDele call, BetDele raise, BetDele fold);
	void StartGettingInput();
	void EndGettingInput();
}