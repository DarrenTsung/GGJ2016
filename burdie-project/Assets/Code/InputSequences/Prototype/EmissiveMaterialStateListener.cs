using DT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveMaterialStateListener : MonoBehaviour {
    // PRAGMA MARK - Internal
    private MeshRenderer _meshRenderer;

    private void Awake() {
        InputKeyFrameListener listener = this.GetComponent<InputKeyFrameListener>();
        listener.OnStateChange.AddListener(this.HandleStateChange);

        this._meshRenderer = this.GetRequiredComponent<MeshRenderer>();
        this._meshRenderer.sharedMaterials[0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
        this._meshRenderer.sharedMaterials[1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
    }

    private void Start() {
        this._meshRenderer.sharedMaterials[0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
        this._meshRenderer.sharedMaterials[1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.4f));
    }

    private void HandleStateChange(OnOffState state) {
        this.StopAllCoroutines();
        if (state == OnOffState.ON) {
            this.StartCoroutine(this.AnimateEmissiveColor(0.4f, 1.0f, 0.2f));
        } else {
            this.StartCoroutine(this.AnimateEmissiveColor(1.0f, 0.4f, 0.2f));
        }
    }

    private IEnumerator AnimateEmissiveColor(float startAlpha, float endAlpha, float duration) {
		for (float time = 0.0f; time < duration; time += Time.deltaTime) {
            // Debug.Log("time: " + time);
			float currentAlpha = Easers.Ease(EaseType.QuadOut, startAlpha, endAlpha, time, duration);
            this._meshRenderer.sharedMaterials[0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, currentAlpha));
            this._meshRenderer.sharedMaterials[1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, currentAlpha));

			yield return null;
		}
        this._meshRenderer.sharedMaterials[0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, endAlpha));
        this._meshRenderer.sharedMaterials[1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, endAlpha));
    }
}