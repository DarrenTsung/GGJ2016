using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InputSequencePlayer : MonoBehaviour {
    // PRAGMA MARK - Public Interface
    public UnityEvent OnStartPlay = new UnityEvent();
    public UnityEvent OnStopPlay = new UnityEvent();
    public InputKeyframeEvent OnKeyframePlayed = new InputKeyframeEvent();

    public void PlaySequence() {
        if (!this.HasSequence()) {
            Debug.LogWarning("PlaySequence: called when we don't have a sequence!");
            return;
        }

        // play first keyframe immediately
        this._delayUntilNextIndex = 0.0f;

        this._currentSequenceIndex = 0;
        this._playing = true;
        this.OnStartPlay.Invoke();
    }

    public void SetupWithSequence(InputSequence sequence) {
        this._currentSequence = sequence;
    }

    public bool HasSequence() {
        return this._currentSequence != null;
    }

    // PRAGMA MARK - Internal
    [SerializeField]
    private int _currentSequenceIndex = 0;
    [SerializeField]
    private bool _playing = false;
    [SerializeField]
    private float _delayUntilNextIndex = 0.0f;

    private InputSequence _currentSequence;

    private void Update() {
        if (this._playing) {
            if (this._delayUntilNextIndex <= 0.0f) {
                this.PlayCurrentIndex();
                this._currentSequenceIndex++;
                if (this._currentSequenceIndex > this._currentSequence.GetKeyFrameCount()) {
                    this.StopPlaying();
                }
            }
            this._delayUntilNextIndex -= Time.deltaTime;
        }
    }

    private void PlayCurrentIndex() {
        this.OnKeyframePlayed.Invoke(this._currentSequence.GetKeyFrameForIndex(this._currentSequenceIndex));
    }

    private void StopPlaying() {
        this._playing = false;
        this.OnStopPlay.Invoke();
    }
}