using DT;
using UnityEngine;
using InControl;

public enum OnOffState {
    ON,
    OFF
}

public class InputKeyFrameListener : MonoBehaviour {
    // PRAGMA MARK - Internal
    [SerializeField]
    private InputControlType _type;
    [SerializeField]
    private Material _onMaterial;
    [SerializeField]
    private Material _offMaterial;

    private OnOffState State {
        set {
            Material materialToUse;
            if (value == OnOffState.ON) {
                materialToUse = this._onMaterial;
            } else {
                materialToUse = this._offMaterial;
            }

            this.GetComponent<MeshRenderer>().sharedMaterial = materialToUse;
        }
    }

    private void Awake() {
        this.State = OnOffState.OFF;
        InputSequencePlayer.OnKeyFramePlayed.AddListener(this.HandleKeyFramePlayed);
        InputSequenceValidator.OnKeyPressed.AddListener(this.HandleKeyPressedDuringValidation);
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

        this.State = OnOffState.ON;

        this.DoAfterDelay(GameConstants.Instance.kVisualizerTurnOnTime, () => {
            this.State = OnOffState.OFF;
        });
    }
}
