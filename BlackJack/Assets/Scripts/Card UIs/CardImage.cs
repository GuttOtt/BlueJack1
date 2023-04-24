using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * 카드 이미지. CardData를 사용해 이미지를 Darw한다.
 * 
 */

public class CardImage : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private Image numberImage, suitImage, iconImage, cardImage;
    private CardData data;
    private Vector2 imageSize;
    public string Name { get => CardIconCSV.GetNameByID(data.ID); }
    public string Desc { get => CardIconCSV.GetDescByID(data.ID); }
    public CardData GetData { get => data; }
    public Vector2 ImageSize {
        get { return imageSize; }
        set {
            imageSize = value;
            ChangeImageSizes();
        }
    }
    public Action onClick;

    private void Awake() {
        cardImage = GetComponent<Image>();
        if (!cardImage)
            cardImage = gameObject.AddComponent<Image>();

        cardImage.sprite = Resources.Load<Sprite>("Sprites/BlankCard");

        if (!numberImage) {
            numberImage = CreateSubImage("Number Image");
        }
        if (!suitImage) {
            suitImage = CreateSubImage("Suit Image");
        }
        if (!iconImage) {
            iconImage = CreateSubImage("Icon Image");
        }
    }

    private Image CreateSubImage(string name) {
        Rect rect = GetComponent<RectTransform>().rect;
        Vector2 size = new Vector2(rect.width, rect.height);
        GameObject obj = new GameObject(name);
        RectTransform objRect = obj.AddComponent<RectTransform>();
        objRect.SetParent(this.transform);
        objRect.sizeDelta = size;
        objRect.localPosition = Vector2.zero;

        return obj.AddComponent<Image>();
    }

    private void ChangeImageSizes() {
        GetComponent<RectTransform> ().sizeDelta = imageSize;
        numberImage.GetComponent<RectTransform>().sizeDelta = imageSize;
        suitImage.GetComponent<RectTransform>().sizeDelta = imageSize;
        iconImage.GetComponent<RectTransform>() .sizeDelta = imageSize;
    }

    public void Draw(CardData data) {
        this.data = data;
        numberImage.sprite = CardSpriteContainer.NumberToSprite(data.number);
        suitImage.sprite = CardSpriteContainer.SuitToSprite(data.suit);
        iconImage.sprite = data.iconPrefab.GetComponent<SpriteRenderer>().sprite;

        numberImage.color = data.suit == Suit.Spade || data.suit == Suit.Club ? Color.black : Color.red;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }
}
