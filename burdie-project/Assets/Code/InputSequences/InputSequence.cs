using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputKeyframeEvent : UnityEvent<InputKeyframe> { }

[Serializable]
public class InputKeyframe {
    public KeyCode key;
    public float playingSequenceDelay;
}

[Serializable]
public class InputSequence {
    public InputKeyframe GetKeyFrameForIndex(int index) {
        return this.inputSequence[index];
    }

    public int GetKeyFrameCount() {
        return this.inputSequence.Count;
    }

    // PRAGMA MARK - Internal
    [SerializeField]
    private List<InputKeyframe> inputSequence = new List<InputKeyframe>();
}

[Serializable]
public class InputSequenceList {
    public List<InputSequence> inputSequenceList = new List<InputSequence>();
}