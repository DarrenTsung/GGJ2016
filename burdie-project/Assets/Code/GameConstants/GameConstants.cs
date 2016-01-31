using DT;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameConstants : Singleton<GameConstants> {
    public float kPlayNextSequenceDelay = 2.5f;

    public float kVisualizerTurnOnTime = 1.2f;

    public float kPlayNextKeyFrameMinDelay = 0.7f;
    public float kPlayNextKeyFrameMaxDelay = 3.0f;

    public string EventNameForStarKey(InputControlType key) {
        return this._starEventMapping[key];
    }

    public string EventNameForFluteKey(InputControlType key) {
        return this._fluteEventMapping[key];
    }

    private Dictionary<InputControlType, string> _starEventMapping;
    private Dictionary<InputControlType, string> _fluteEventMapping;

    private void Awake() {
        this._starEventMapping = new Dictionary<InputControlType, string> {
            { InputControlType.Action1, "Play_Star_1" },
            { InputControlType.Action2, "Play_Star_2" },
            { InputControlType.Action3, "Play_Star_3" },
            { InputControlType.Action4, "Play_Star_4" },
        };

        this._fluteEventMapping = new Dictionary<InputControlType, string> {
            { InputControlType.Action1, "Play_Flute_Note_1" },
            { InputControlType.Action2, "Play_Flute_Note_1" },
            { InputControlType.Action3, "Play_Flute_Note_3" },
            { InputControlType.Action4, "Play_Flute_Note_4" },
        };
    }
}