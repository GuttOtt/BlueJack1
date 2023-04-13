using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum EffectSituation {
	None, OnOpen, OnEveryHit, OnWin, OnLose, OnFold, OnBurst,
	OnRaise, OnBlackJack, OnShowDown, OnDiscard, OnHiddenOpen
}


[System.Serializable]
public class ComponentStorage : SerializableDictionary.Storage<Component[]> { }
[System.Serializable]
public class SituationComponentDictionary 
	: SerializableDictionary<EffectSituation, Component[], ComponentStorage> { }

public class CardIcon : MonoBehaviour {
	[SerializeField] GameObject activateAnimation;
	[SerializeField] private int id;
	[SerializeField] private SituationComponentDictionary situationEffectArrayDict;
	public int ID { get => id; }
    public BlackJacker Owner { 
		get => transform.parent.GetComponent<Card>().owner;
	}


	public IEnumerator TryToActivate(EffectSituation situation) {
		foreach (EffectSituation key in situationEffectArrayDict.Keys) {
			if (key == situation) {
				Component[] components = situationEffectArrayDict[key];
				foreach (Component component in components) {
                    ActivateAnimation();
                    yield return new WaitForSeconds(1f);
                    ICardEffect effect = component as ICardEffect;
					effect.Activate();
				}
			}
		}
	}

	private void ActivateAnimation() {
		Instantiate(activateAnimation, transform);
	}
}