using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Hand : MonoBehaviour {
	[SerializeField] private GameObject handParent;
	[SerializeField] private GameObject hiddenParent;
	[SerializeField] private bool isPlayerHand;
	private Discards discards;
	private List<Card> cards {
		get {
			List<Card> cards = new List<Card>();
			cards.AddRange(field);
			cards.AddRange(hiddens);
			return cards;
		}
	}
	private List<Card> hiddens = new List<Card>();
	private List<Card> field = new List<Card>();
	private bool isForcedToBlackjack = false;
	public bool IsHiddenShown {
		get {
			bool isAllFront = true;
			foreach (Card card in hiddens) {
				if (!card.IsFront) isAllFront = false;
			}
			return isAllFront;
		}
	}

	private void Awake() {
		discards = GetComponent<Discards>();
		TurnEventBus.Subscribe(TurnEventType.NEW_ROUND, UnforceBlackjack);
	}

	private void ArrangeField() {
		int q = field.Count / 2;
		int r = field.Count % 2;

		Vector3 origin;

		if (r == 0) {
			origin = handParent.transform.position + Vector3.left * (q - 0.5f) * 2;
		}
		else {
			origin = handParent.transform.position + Vector3.left * q * 2;
		}

        for (int i = 0; i < field.Count; i++) {
            field[i].MoveTo(origin + Vector3.right * i * 2);
        }
    }

	public void AddCard(Card card) {
		field.Add(card);
		card.transform.SetParent(handParent.transform);
		card.IsFront = true;
        StartCoroutine(card.ActivateIcon(EffectSituation.OnOpen));
		StartCoroutine(ActivateAllField(EffectSituation.OnEveryHit));
        ArrangeField();
	}

	public void AddHidden(Card card) {
		hiddens.Add(card);
		card.transform.SetParent(hiddenParent.transform);
		card.MoveTo(hiddenParent.transform.position + Vector3.right 
					* (hiddens.Count - 1.5f) * 2);

		if (isPlayerHand) {
			card.IsFront = true;
		}
		else {
			card.IsFront = false;
		}
	}

	public int GetTotal() {

		int total = 0;
		foreach (Card card in cards) {
			total += card.GetNumber();
		}

		//ForcedBlackjack 상태이더라도 버스트는 감지해야함. 따라서 total <= 21
		if (isForcedToBlackjack && total <= 21 ) { return 21; }

        return total;
	}

	public int GetFieldTotal() {
		int total = 0;
        foreach (Card card in field) {
            total += card.GetNumber();
        }
        return total;
    }

	public bool IsBursted() {
		if (GetTotal() > 21)
			return true;
		return false;
	}

	public void DiscardAll() {
		for (int i = field.Count - 1; i >= 0; i--) {
			Card card = field[i];
			Discard(card);
		}
		
		for (int i = hiddens.Count - 1; i >= 0; i--) {
			Card card = hiddens[i];
			Discard(card);
		}
	}

	public void Discard(Card card) {
		discards.AddCard(card);
		card.SetActiveCardBackMask(false);

		if (field.Contains(card)) {
			field.Remove(card);
		}
		else if (hiddens.Contains(card)) {
			hiddens.Remove(card);
		}

		StartCoroutine(card.ActivateIcon(EffectSituation.OnDiscard));
	}

	public IEnumerator OpenHiddens() {
		//히든 캐싱
		List<Card> tempHiddens = new List<Card>();
		for (int i = 0; i < hiddens.Count; i++) tempHiddens.Add(hiddens[i]);
        
		//히든을 필드로 옮기고 앞면으로
		foreach (Card card in hiddens) {
            field.Add(card);
			card.SetActiveCardBackMask(false);
        }
		ShowHiddens();
		ArrangeField();
        hiddens.Clear();

		yield return new WaitForSeconds(0.3f);

        //캐싱한 히든에 대해 OnOpen, OnHiddenOpen
        for (int i = 0; i < tempHiddens.Count; i++) {
			Card card = tempHiddens[i];
            yield return StartCoroutine(card.ActivateIcon(EffectSituation.OnOpen));
            yield return StartCoroutine(card.ActivateIcon(EffectSituation.OnHiddenOpen));
        }
	}

	//Open과는 달리, 카드의 효과를 발동하지 않고 상대에게 보여주기만 함.
	//Burst를 했다면, Hidden 카드는 Open이 아닌 Show가 된다.
	public void ShowHiddens() {
		foreach (Card card in hiddens) {
			card.IsFront = true;
		}
	}

	public void PutHiddenOnBoard() {
        List<Card> cardList = cards;

        int q = cardList.Count / 2;
        int r = cardList.Count % 2;

        Vector3 origin;

        if (r == 0) {
            origin = handParent.transform.position + Vector3.left * (q - 0.5f) * 2;
        }
        else {
            origin = handParent.transform.position + Vector3.left * q * 2;
        }

        for (int i = 0; i < cardList.Count; i++) {
			Vector3 movePos = origin + Vector3.right * i * 2;
			if (hiddens.Contains(cardList[i])) {
				movePos += Vector3.right * 0.5f;
				
				if (isPlayerHand) {
					cardList[i].SetActiveCardBackMask(true);
                }
			}
            cardList[i].MoveTo(movePos);
        }
    }

	public IEnumerator ActivateAllIcon(EffectSituation situation) {
        for (int i = 0; i < cards.Count; i++) {
            Card card = cards[i];
            yield return StartCoroutine(card.ActivateIcon(situation));
        }
    }

	public IEnumerator ActivateAllField(EffectSituation situation) {
		for (int i = 0; i < field.Count; i++) {
			Card card = field[i];
            yield return StartCoroutine(card.ActivateIcon(situation));
        }
	}

	public IEnumerator ActivateAllHidden(EffectSituation situation) {
        for (int i = 0; i < hiddens.Count; i++) {
            Card card = hiddens[i];
            yield return StartCoroutine(card.ActivateIcon(situation));
        }
    }

	public Card GetRandomField() {
		return field[Random.Range(0, field.Count)];
	}

	//field 에서 인자를 제외한 나머지 중 랜덤
	public Card GetRandomField(Card card) {
		List<Card> fieldExcept = field.ToList();
		fieldExcept.Remove(card);
		if (fieldExcept.Count == 0)
			return card;
		return fieldExcept[Random.Range(0, fieldExcept.Count)];
	}

	public void UnforceBlackjack() {
		isForcedToBlackjack= false;
	}

	public void ForceToBlackjack() {
		isForcedToBlackjack= true;
	}

	public int GetNumberOfSuitInField(Suit suit) {
		int n = 0;

		foreach(Card card in field) {
			if (card.GetSuit() == suit) n++;
		}

		return n;
	}

	public int GetNumberOfCard() {
		Debug.Log("Number of Card is " + cards.Count);
		return cards.Count;
	}
}
