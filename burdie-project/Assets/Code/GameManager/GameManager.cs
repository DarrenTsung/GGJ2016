using DT;
using UnityEngine;
using UnityEngine.Events;

public enum Intensity {
    LOW = 0,
    MEDIUM = 1,
    HIGH = 2
}

public class GameManager : Singleton<GameManager> {
    // PRAGMA MARK - Public Interface
    public static UnityEvent OnGameEnd = new UnityEvent();

    public bool IsGameFinished() {
        return this._triesLeft < 0;
    }

    [SerializeField]
    private Intensity _intensity = Intensity.LOW;
    public Intensity CurrentIntensity {
        get {
            return this._intensity;
        }
        private set {
            this._intensity = value;
        }
    }

    // PRAGMA MARK - Internal
    [SerializeField, ReadOnly]
    private int _triesLeft = 3;
    [SerializeField, ReadOnly]
    private int _numberOfCompletedSequences = 0;

    private void Awake() {
        this.CurrentIntensity = Intensity.LOW;
        InputSequenceValidator.OnSuccessValidate.AddListener(this.HandleSuccessfulValidation);
        InputSequenceValidator.OnFailureValidate.AddListener(this.HandleFailedValidation);
    }

    private void IncreaseIntensity() {
        if (this.CurrentIntensity == Intensity.HIGH) {
            return;
        }

        this.CurrentIntensity = (Intensity)(((int)this.CurrentIntensity) + 1);
    }

    private void HandleSuccessfulValidation() {
        this._numberOfCompletedSequences++;
    }

    private void HandleFailedValidation() {
        this._triesLeft--;
        this.IncreaseIntensity();
        if (this.IsGameFinished()) {
            GameManager.OnGameEnd.Invoke();
        }
    }
}
