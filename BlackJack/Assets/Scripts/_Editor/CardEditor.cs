using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour {
	[SerializeField] private Canvas canvas;
	[SerializeField] private Button[] numButtons = new Button[7];
	[SerializeField] private Button[] suitButtons = new Button[4];
	[SerializeField] private IconSlot[] iconSlots = new IconSlot[5];
	[SerializeField] private CardIcon[] cardIconPrefabs;
	[SerializeField] private Button closeButton, completeButton;
	[SerializeField] private CopyCardImage copyImg;
	[SerializeField] private Button leftButton, rightButton;
	private EditingCard card;
	private CardData data;
	private int iconSlotIndex;

	
	private void Start() {
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

		cardIconPrefabs = Resources.LoadAll<CardIcon>("CardIcons");

		leftButton.onClick.AddListener( () => MoveIconSlot(-1) );
		rightButton.onClick.AddListener( () => MoveIconSlot(1) );

		iconSlotIndex = 0;
		SetIconSlot();

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

	public void ChangeIcon(CardIcon iconPrefab) {
		data.ChangeIcon(iconPrefab);
		data.PasteTo(copyImg);
	}

	private void MoveIconSlot(int direction) {
		if (iconSlotIndex + direction < 0 || iconSlotIndex + 4 + direction > cardIconPrefabs.Length -1 )
			return;

		iconSlotIndex += direction;
		SetIconSlot();
	}

	private void SetIconSlot() {
		for (int i = 0; i < 5; i++) {
			iconSlots[i].Initialize(this);
			iconSlots[i].SetIcon(cardIconPrefabs[iconSlotIndex + i]); 
		}
	}
}