using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapControl {
	bool IsConsidering(); //For AI Delay. PlayerSC Always return false.
}
