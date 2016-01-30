using DT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class InputSequencePlayer : MonoBehaviour {
    // PRAGMA MARK - Public Interface
    public static UnityEvent OnStartPlay = new UnityEvent();
    public static UnityEvent OnStopPlay = new UnityEvent();
    public static InputKeyFrameEvent OnKeyFramePlayed = new InputKeyFrameEvent();

    public void PlaySequence() {
        if (!this.HasSequence()) {
            Debug.LogWarning("PlaySequence: called when we don't have a sequence!");
            return;
        }

        if (this._playing) {
            Debug.LogWarning("PlaySequence: called while playing!");
            return;
        }

        this.RandomizeCurrentSequence();

        // play first keyframe immediately
        this._delayUntilNextIndex = 0.0f;

        this._currentSequenceIndex = 0;
        this._playing = true;
        Debug.Log("START PLAYING Sequence!");
        InputSequencePlayer.OnStartPlay.Invoke();
    }

    public void SetupWithSequence(InputSequence sequence) {
        this._currentSequence = sequence;
    }

    public bool HasSequence() {
        return this._currentSequence != null;
    }

    // PRAGMA MARK - Internal
    [SerializeField, ReadOnly]
    private int _currentSequenceIndex = 0;
    [SerializeField, ReadOnly]
    private bool _playing = false;
    [SerializeField, ReadOnly]
    private float _delayUntilNextIndex = 0.0f;

    private InputSequence _currentSequence;

    private void Update() {
        if (this._playing) {
            if (this._delayUntilNextIndex <= 0.0f) {
                this.PlayCurrentIndex();
                this._currentSequenceIndex++;
                if (this._currentSequenceIndex >= this._currentSequence.GetKeyFrameCount()) {
                    this.StopPlaying();
                }
            }
            this._delayUntilNextIndex -= Time.deltaTime;
        }
    }

    // NOTE (darren): decided that randomization was more fun then just playing same sequences over and over
    private void RandomizeCurrentSequence() {
        Dictionary<InputControlType, int> weightedControlTypeMap = new Dictionary<InputControlType, int> {
            { InputControlType.Action1, 60 },
            { InputControlType.Action2, 50 },
            { InputControlType.Action3, 40 },
            { InputControlType.Action4, 50 }
        };

        for (int i = 0; i < this._currentSequence.GetKeyFrameCount(); i++) {
            InputKeyFrame keyframe = this._currentSequence.GetKeyFrameForIndex(i);
            keyframe.key = weightedControlTypeMap.PickRandomWeighted();
        }
    }

    private void PlayCurrentIndex() {
        InputKeyFrame keyframe = this._currentSequence.GetKeyFrameForIndex(this._currentSequenceIndex);
        Debug.Log("keyframe.key: " + keyframe.key);
        InputSequencePlayer.OnKeyFramePlayed.Invoke(keyframe);
        // TODO (darren): consider if this should be random or manually input
        this._delayUntilNextIndex = Random.Range(1.0f, 3.0f);
    }

    private void StopPlaying() {
        this._playing = false;
        Debug.Log("STOP PLAYING Sequence!");
        InputSequencePlayer.OnStopPlay.Invoke();
    }
}