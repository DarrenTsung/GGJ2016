using DT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontUseControllerBehaviour : MonoBehaviour {
    private Image _image;

    private void Awake() {
        this._image = this.GetComponent<Image>();
        InputSequencePlayer.OnStartPlay.AddListener(this.HandlePlayerStartPlay);
        InputSequencePlayer.OnStopPlay.AddListener(this.HandlePlayerStopPlay);
    }

    private void HandlePlayerStartPlay() {
        this.StopAllCoroutines();
        this.StartCoroutine(this.AnimateAlphaColor(0.0f, 0.1f, 0.3f));
    }

    private void HandlePlayerStopPlay() {
        this.StopAllCoroutines();
        this.StartCoroutine(this.AnimateAlphaColor(0.1f, 0.0f, 0.3f));
    }

    private IEnumerator AnimateAlphaColor(float startAlpha, float endAlpha, float duration) {
		for (float time = 0.0f; time < duration; time += Time.deltaTime) {
			float currentAlpha = Easers.Ease(EaseType.QuadOut, startAlpha, endAlpha, time, duration);
            this._image.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);

			yield return new WaitForEndOfFrame();
		}
        this._image.color = new Color(1.0f, 1.0f, 1.0f, endAlpha);
    }
}