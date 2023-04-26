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
	private Vector2 cardSize;
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

		Sprite blankCard = Resources.Load<Sprite>("Sprites/BlankCard");

		TurnEventBus.Subscribe(TurnEventType.NEW_ROUND, UnforceBlackjack);

        Sprite cardSprite = Resources.Load<Sprite>("Sprites/BlankCard");
        cardSize = cardSprite.bounds.size;
    }

	private void ArrangeField() {
		int q = field.Count / 2;
		int r = field.Count % 2;

		Vector3 origin;

		if (r == 0) {
			origin = handParent.transform.position 
				+ Vector3.left * (q - 0.5f) * cardSize.x;
		}
		else {
			origin = handParent.transform.position 
				+ Vector3.left * q * cardSize.x;
		}

        for (int i = 0; i < field.Count; i++) {
            field[i].MoveTo(origin + Vector3.right * i * cardSize.x);
        }
    }

	public void AddCard(Card card) {
		field.Add(card);
		card.transform.SetParent(handParent.transform);
		card.IsFront = true;
        ArrangeField();

		StartCoroutine(AddCardCR(card));
	}

	public IEnumerator AddCardCR(Card card) {
        yield return StartCoroutine(card.ActivateIcon(EffectSituation.OnOpen));
        yield return StartCoroutine(ActivateAllField(EffectSituation.OnEveryHit));
    }

	public void AddHidden(Card card) {
		hiddens.Add(card);
		card.transform.SetParent(hiddenParent.transform);
		card.MoveTo(hiddenParent.transform.position 
			+ Vector3.right * (hiddens.Count - 1.5f) * cardSize.x);

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

		//ForcedBlackjack �����̴��� ����Ʈ�� �����ؾ���. ���� total <= 21
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
		//���� ĳ��
		List<Card> tempHiddens = new List<Card>();
		for (int i = 0; i < hiddens.Count; i++) tempHiddens.Add(hiddens[i]);
        
		//������ �ʵ�� �ű�� �ո�����
		foreach (Card card in hiddens) {
            field.Add(card);
			card.SetActiveCardBackMask(false);
        }
		ShowHiddens();
		ArrangeField();
        hiddens.Clear();

		yield return new WaitForSeconds(0.3f);

        //ĳ���� ���翡 ���� OnOpen, OnHiddenOpen
        for (int i = 0; i < tempHiddens.Count; i++) {
			Card card = tempHiddens[i];
            yield return StartCoroutine(card.ActivateIcon(EffectSituation.OnOpen));
            yield return StartCoroutine(card.ActivateIcon(EffectSituation.OnHiddenOpen));
        }
	}

	//Open���� �޸�, ī���� OnHiddenOpen �� �ߵ����� ���� ��뿡�� �����ֱ⸸ ��.
	//Burst�� �ߴٸ�, Hidden ī��� Open�� �ƴ� Show�� �ȴ�.
	public void ShowHiddens() {
		foreach (Card card in hiddens) {
			card.IsFront = true;
			card.SetActiveCardBackMask(false);
		}
	}

	public void PutHiddenOnBoard() {
        List<Card> cardList = cards;

        int q = cardList.Count / 2;
        int r = cardList.Count % 2;

        Vector3 origin;

        if (r == 0) {
            origin = handParent.transform.position 
				+ Vector3.left * (q - 0.5f) * cardSize.x;
        }
        else {
            origin = handParent.transform.position 
				+ Vector3.left * q * cardSize.x;
        }

        for (int i = 0; i < cardList.Count; i++) {
			Vector3 movePos = origin + Vector3.right * i * cardSize.x;
			if (hiddens.Contains(cardList[i])) {
				movePos += Vector3.right * 0.5f * cardSize.x;
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

	//field ���� ���ڸ� ������ ������ �� ����
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

	public void SetActiveCardBackMaskOnHidden() {
		foreach (Card card in hiddens) {
			card.SetActiveCardBackMask(true);
		}
	}

	public void SetHiddensBack() {
		foreach( Card card in hiddens) {
			card.IsFront = false;
		}
	}
}
