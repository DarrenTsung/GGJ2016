using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class InputKeyFrameEvent : UnityEvent<InputKeyFrame> { }

[Serializable]
public class InputKeyFrame {
    public InputControlType key;
    public float playingSequenceDelay;
}

[Serializable]
public class InputSequence {
    public InputKeyFrame GetKeyFrameForIndex(int index) {
        return this._inputSequence[index];
    }

    public int GetKeyFrameCount() {
        return this._inputSequence.Count;
    }

    // PRAGMA MARK - Internal
    [SerializeField]
    private List<InputKeyFrame> _inputSequence = new List<InputKeyFrame>();
}

[Serializable]
public class InputSequenceList {
    public InputSequence GetSequenceForIndex(int index) {
        return this._inputSequenceList[index];
    }

    [SerializeField]
    private List<InputSequence> _inputSequenceList = new List<InputSequence>();
}