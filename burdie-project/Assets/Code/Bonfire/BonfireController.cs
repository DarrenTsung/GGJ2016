using DT;
using System.Collections;
using UnityEngine;

public class BonfireController : MonoBehaviour {
    // PRAGMA MARK - Internal
    private Intensity _currentIntensity;

    [SerializeField]
    private GameObject _lowIntensityBonfire;
    [SerializeField]
    private GameObject _mediumIntensityBonfire;
    [SerializeField]
    private GameObject _highIntensityBonfire;

    private void Awake() {
        GameManager.OnIntensityChange.AddListener(this.HandleIntensityChange);
    }

    private void HandleIntensityChange(Intensity newIntensity) {
        if (newIntensity == this._currentIntensity) {
            this.SetIntensityBonfireActive(this._currentIntensity, true);
            return;
        }

        this.SetIntensityBonfireActive(this._currentIntensity, false, 1.5f);
        this._currentIntensity = newIntensity;
        this.SetIntensityBonfireActive(this._currentIntensity, true);
    }

    private void SetIntensityBonfireActive(Intensity intensity, bool active, float delay = 0.0f) {
        this.DoAfterDelay(delay, () => {
            switch (intensity) {
                case Intensity.LOW:
                    this._lowIntensityBonfire.SetActive(active);
                    break;
                case Intensity.MEDIUM:
                    this._mediumIntensityBonfire.SetActive(active);
                    break;
                case Intensity.HIGH:
                default:
                    this._highIntensityBonfire.SetActive(active);
                    break;
            }
        });
    }
}