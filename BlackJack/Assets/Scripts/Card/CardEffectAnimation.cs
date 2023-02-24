using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectAnimation : MonoBehaviour
{
	private Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	public void Update() {
		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
			Destroy(this.gameObject);
		}
	}
}
