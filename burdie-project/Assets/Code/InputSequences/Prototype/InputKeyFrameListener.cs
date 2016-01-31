using DT;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public enum OnOffState {
    ON,
    OFF
}

public class OnOffStateEvent : UnityEvent<OnOffState> {}

public class InputKeyFrameListener : MonoBehaviour {
    public static UnityEvent BecameOffState = new UnityEvent();
    public OnOffStateEvent OnStateChange = new OnOffStateEvent();

    // PRAGMA MARK - Internal
    [SerializeField]
    private InputControlType _type;

    private OnOffState _state;
    private OnOffState State {
        set {
            if (this._state == value) {
                return;
            }

            this._state = value;
            this.OnStateChange.Invoke(this._state);
            if (this._state == OnOffState.OFF) {
                InputKeyFrameListener.BecameOffState.Invoke();
            }
        }
    }

    private void Awake() {
        InputSequencePlayer.OnKeyFramePlayed.AddListener(this.HandleKeyFramePlayed);
        InputSequenceValidator.OnKeyPressed.AddListener(this.HandleKeyPressedDuringValidation);
    }

    private void Start() {
        this.State = OnOffState.OFF;
    }

    private void HandleKeyFramePlayed(InputKeyFrame keyframe) {
        this.TurnOnIfKeyMatches(keyframe.key);
    }

    private void HandleKeyPressedDuringValidation(InputControlType key) {
        this.TurnOnIfKeyMatches(key);
    }

    private void TurnOnIfKeyMatches(InputControlType key) {
        if (key != this._type) {
            return;
        }

        this.StopAllCoroutines();

        this.State = OnOffState.ON;
        AkSoundEngine.PostEvent(GameConstants.Instance.EventNameForStarKey(this._type), this.gameObject);

        this.DoAfterDelay(GameConstants.Instance.kVisualizerTurnOnTime, () => {
            this.State = OnOffState.OFF;
        });
    }
}
