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
    }

    private void HandleStateChange(OnOffState state) {
        this.StopAllCoroutines();
        if (state == OnOffState.ON) {
            this._meshRenderer.enabled = true;
            this.StartCoroutine(this.AnimateEmissiveColor(0.2f, 0.8f, 0.6f));
        } else {
            this._meshRenderer.enabled = false;
            this.StartCoroutine(this.AnimateEmissiveColor(0.8f, 0.2f, 0.6f));
        }
    }

    private IEnumerator AnimateEmissiveColor(float startAlpha, float endAlpha, float duration) {
		for (float time = 0.0f; time < duration; time += Time.deltaTime) {
			float currentAlpha = Easers.Ease(EaseType.QuadOut, startAlpha, endAlpha, time, duration);
            // this._meshRenderer.sharedMaterial.EnableKeyword ("_EMISSION");
            // this._meshRenderer.sharedMaterial.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, currentAlpha));
            DynamicGI.SetEmissive(this._meshRenderer, new Color(currentAlpha, currentAlpha, currentAlpha, currentAlpha));
            Debug.Log("currentAlpha: " + currentAlpha);

			yield return new WaitForEndOfFrame();
		}
    }
}