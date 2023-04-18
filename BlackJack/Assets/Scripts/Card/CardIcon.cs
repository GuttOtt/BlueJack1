using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

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
	[SerializeField] private IEffectCondition effectCondition;
	public int ID { get => id; }
    public BlackJacker Owner { 
		get => transform.parent.GetComponent<Card>().owner;
	}


    private void Awake() {
		effectCondition = GetComponent<IEffectCondition>();
    }

    public IEnumerator TryToActivate(EffectSituation situation) {
		if (effectCondition != null && !effectCondition.IsSatisfied()) {
			yield break;
		}

		foreach (EffectSituation key in situationEffectArrayDict.Keys) {
			if (key == situation) {
				Component[] components = situationEffectArrayDict[key];
				foreach (Component component in components) {
                    ActivateAnimation();
                    yield return new WaitForSeconds(1.5f);
                    ICardEffect effect = component as ICardEffect;
					effect.Activate();
				}
			}
		}
	}

	private void ActivateAnimation() {
		if (activateAnimation)
			Instantiate(activateAnimation, transform);
	}
}