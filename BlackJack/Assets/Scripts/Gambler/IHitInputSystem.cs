using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitDele();

public interface IHitInputSystem {
	void SetInput(HitDele hit, HitDele stay);
	void StartGettingInput();
	void EndGettingInput();
}
