using DT;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using InControl;

public class InputSequenceValidator : MonoBehaviour {
    // PRAGMA MARK - Public Interface
    public static UnityEvent OnStartValidate = new UnityEvent();
    public static UnityEvent OnSuccessValidate = new UnityEvent();
    public static UnityEvent OnFailureValidate = new UnityEvent();
    public static InputKeyFrameEvent OnKeyFrameValidated = new InputKeyFrameEvent();

    public void SetupWithSequence(InputSequence sequence) {
        this._sequence = sequence;
    }

    public void StartValidatingSequence() {
        Debug.Log("Start Validating Sequence!");
        this._currentSequenceIndex = 0;
        this._validating = true;
        this._validControlType = this._sequence.GetKeyFrameForIndex(this._currentSequenceIndex).key;
    }


    // PRAGMA MARK - Internal
	private void Update() {
        if (this._validating) {
            InputControlType[] typesPressed = this.AllInputControlTypesPressedThisFrame();
            if (typesPressed.Length > 0) {
                // if the valid control type was found, then we succeed
                if (Array.Exists(typesPressed, t => t == this._validControlType)) {
                    this.HandleSuccessfulInput();
                } else {
                    this.HandleValidationFailure();
                }
            }
        }
	}

    private void HandleSuccessfulInput() {
        InputKeyFrame successfulKeyFrame = this._sequence.GetKeyFrameForIndex(this._currentSequenceIndex);
        InputSequenceValidator.OnKeyFrameValidated.Invoke(successfulKeyFrame);
        Debug.Log("Successfully validated: " + successfulKeyFrame.key + "!");

        this._currentSequenceIndex++;
        // if we don't have any more keyframes to validate, the player successfully put in the input sequence!
        if (this._currentSequenceIndex >= this._sequence.GetKeyFrameCount()) {
            this.HandleValidationSuccess();
        } else {
            // if we still have keyframes, move on to the next one
            this._validControlType = this._sequence.GetKeyFrameForIndex(this._currentSequenceIndex).key;
        }
    }

    private void HandleValidationSuccess() {
        Debug.Log("Success!");
        this._validating = false;
        InputSequenceValidator.OnSuccessValidate.Invoke();
    }

    private void HandleValidationFailure() {
        Debug.Log("Failure!");
        this._validating = false;
        InputSequenceValidator.OnFailureValidate.Invoke();
    }

    private InputControlType[] AllInputControlTypesPressedThisFrame() {
        InputControlType[] types = {
            InputControlType.Action1,
            InputControlType.Action2,
            InputControlType.Action3,
            InputControlType.Action4,
        };

        List<InputControlType> pressedTypes = new List<InputControlType>();
        foreach (InputControlType type in types) {
    		foreach (InputDevice device in InputManager.Devices) {
    			if (device.GetControl(type).WasPressed) {
                    pressedTypes.Add(type);
    			}
    		}
        }

        return pressedTypes.ToArray();
    }


    // PRAGMA MARK - Internal
    private InputSequence _sequence;

    [SerializeField, ReadOnly]
    private int _currentSequenceIndex = 0;
    [SerializeField, ReadOnly]
    private bool _validating;

    [SerializeField, ReadOnly]
    private InputControlType _validControlType;
}