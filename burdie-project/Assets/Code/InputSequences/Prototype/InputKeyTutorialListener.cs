using DT;
using UnityEngine;
using InControl;

public class InputKeyTutorialListener : MonoBehaviour {
    public bool Disabled = false;

    private void Update() {
        if (this.Disabled) {
            return;
        }

        InputControlType[] typesPressed = InputSequenceValidator.AllInputControlTypesPressedThisFrame();
        if (typesPressed.Length > 0) {
            InputSequenceValidator.OnKeyPressed.Invoke(typesPressed[0]);
        }
    }
}