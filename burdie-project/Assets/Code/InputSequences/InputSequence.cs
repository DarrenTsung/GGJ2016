using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class InputKeyframeEvent : UnityEvent<InputKeyframe> { }

[Serializable]
public class InputKeyframe {
    public InputControlType key;
    public float playingSequenceDelay;
}

[Serializable]
public class InputSequence {
    public InputKeyframe GetKeyFrameForIndex(int index) {
        return this._inputSequence[index];
    }

    public int GetKeyFrameCount() {
        return this._inputSequence.Count;
    }

    // PRAGMA MARK - Internal
    [SerializeField]
    private List<InputKeyframe> _inputSequence = new List<InputKeyframe>();
}

[Serializable]
public class InputSequenceList {
    public InputSequence GetSequenceForIndex(int index) {
        return this._inputSequenceList[index];
    }

    [SerializeField]
    private List<InputSequence> _inputSequenceList = new List<InputSequence>();
}