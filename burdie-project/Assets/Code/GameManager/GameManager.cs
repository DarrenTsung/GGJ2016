using DT;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
    // PRAGMA MARK - Public Interface
    public static UnityEvent OnGameEnd = new UnityEvent();

    public bool IsGameFinished() {
        return this._triesLeft < 0;
    }

    // PRAGMA MARK - Internal
    [SerializeField, ReadOnly]
    private int _triesLeft = 3;
    [SerializeField, ReadOnly]
    private int _numberOfCompletedSequences = 0;

    private void Awake() {
        InputSequenceValidator.OnSuccessValidate.AddListener(this.HandleSuccessfulValidation);
        InputSequenceValidator.OnFailureValidate.AddListener(this.HandleFailedValidation);
    }

    private void HandleSuccessfulValidation() {
        this._numberOfCompletedSequences++;
    }

    private void HandleFailedValidation() {
        this._triesLeft--;
        if (this.IsGameFinished()) {
            GameManager.OnGameEnd.Invoke();
        }
    }
}
