using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour {
	[SerializeField] private Canvas canvas;
	[SerializeField] private Button[] numButtons = new Button[7];
	[SerializeField] private Button[] suitButtons = new Button[4];
	[SerializeField] private IconSlot[] iconSlots = new IconSlot[5];
	[SerializeField] private List<CardIcon> cardIconPrefabs = new List<CardIcon>(); 
	[SerializeField] private Button closeButton, completeButton;
	[SerializeField] private CopyCardImage copyImg;
	private EditingCard card;
	private CardData data;
	private int iconSlotIndex;

	
	private void Awake() {
		closeButton.onClick.AddListener(delegate {ClosePanel();});
		completeButton.onClick.AddListener(CompleteEditing);
		
		for (int i = 0; i < 7; i++) {
			int temp = i; //To prevent Closure Problem. https://mentum.tistory.com/343
			numButtons[i].onClick.AddListener(delegate {ChangeNumber(temp + 1);} );
		}

		for (int i = 0; i < 4; i++) {
			int temp = i; //To prevent Closure Problem. https://mentum.tistory.com/343
			suitButtons[i].onClick.AddListener(delegate {ChangeSuit((Suit) temp);} );
		}

		iconSlotIndex = 0;
		for (int i = 0; i < 5; i++) {
			iconSlots[i].Initialize(data, copyImg);
			iconSlots[i].SetIcon(cardIconPrefabs[i]); 
		}

		ClosePanel();
	}
	
	private void ClosePanel() {
		canvas.gameObject.SetActive(false);
	}

	private void OpenPanel() {
		canvas.gameObject.SetActive(true);
	}

	private void MakeCopyImage() {
		data = card.GetData();
		data.PasteTo(copyImg);
	}

	public void StartEditing(EditingCard selectedCard) {
		OpenPanel();
		this.card = selectedCard;
		MakeCopyImage();
	}


	private void CompleteEditing() {
		data.PasteTo(card);
		ClosePanel();
	}

	private void CancleEditing() {
		ClosePanel();
	}

	private void ChangeNumber(int number) {
		data.ChangeNumber(number);
		data.PasteTo(copyImg);
	}

	private void ChangeSuit(Suit suit) {
		data.ChangeSuit(suit);
		data.PasteTo(copyImg);
	}

	private void ChangeIcon(CardIcon iconPrefab) {
		data.ChangeIcon(iconPrefab);
		data.PasteTo(copyImg);
	}
}