using DT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class InputKeyFrameEvent : UnityEvent<InputKeyFrame> { }

[Serializable]
public class InputKeyFrame {
    // HACK (darren): prevent these from showing up in the editor
    [ReadOnly]
    public InputControlType key;
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

    public int GetSequenceCount() {
        return this._inputSequenceList.Count;
    }

    [SerializeField]
    private List<InputSequence> _inputSequenceList = new List<InputSequence>();
}