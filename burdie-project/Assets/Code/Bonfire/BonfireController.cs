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
        this.SetCurrentIntensityBonfireActive(false);
        this._currentIntensity = newIntensity;
        this.SetCurrentIntensityBonfireActive(true);
    }

    private void SetCurrentIntensityBonfireActive(bool active) {
        switch (this._currentIntensity) {
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
    }
}