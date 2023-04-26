using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitControl : MonoBehaviour {
    [SerializeField] Animation damageAnim;
    [SerializeField] GameObject portrait;
    private Vector2 portraitOrigin;

    private void Awake() {
        portraitOrigin = portrait.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log(gameObject.tag + "" + portraitOrigin);
    }

    private IEnumerator ShakePortrait() {
        int sign = -1;
        float distanceScale = 2f;
        RectTransform portraitTransform = portrait.GetComponent<RectTransform>();
        portraitTransform.anchoredPosition = portraitOrigin;

        for (float distance = 10; distance >= 0; distance--) {
            Vector2 movePos = portraitOrigin + new Vector2(distance * sign * distanceScale, 0);

            portraitTransform.anchoredPosition = movePos;
            sign *= -1;

            yield return new WaitForSeconds((16 - distance) * 0.01f);
        }
    }

    public void AnimateDamageTaken() {
        StartCoroutine(ShakePortrait());
    }


}
