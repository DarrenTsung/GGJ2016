using DT;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[OpenableClass]
[CustomExtensionInspectorAttribute]
public class InputSequenceManager : Singleton<InputSequenceManager> {
    public InputSequenceList inputSequenceList;
    public TextAsset _inputSequenceListSource;

    [Header("Outlets - Set these up!")]
    public InputSequencePlayer _sequencePlayer;
    public InputSequenceValidator _sequenceValidator;

    [MakeButton]
    [OpenableMethod]
    public void ResetAndStartPlayingSequences() {
        this.StopAllCoroutines();
        this._currentIndex = 0;
        this.PlayCurrentInputSequence();
    }


    // PRAGMA MARK - Internal
    [Header("Properties")]
    [SerializeField]
    private int _currentIndex = 0;

    private void Awake() {
        this.DeserializeFromTextAsset();
        InputSequenceValidator.OnSuccessValidate.AddListener(this.HandleSuccessfulValidation);
        InputSequenceValidator.OnFailureValidate.AddListener(this.HandleFailedValidation);
        InputSequencePlayer.OnStopPlay.AddListener(this.ValidateCurrentInputSequence);
    }

    private void HandleSuccessfulValidation() {
        this._currentIndex++;
        if (this._currentIndex >= this.inputSequenceList.GetSequenceCount()) {
            Debug.LogWarning("Reached end of sequence list!");
            this._currentIndex--;
        }

        this.DoAfterDelay(GameConstants.Instance.kPlayNextSequenceDelay, () => {
            this.PlayCurrentInputSequence();
        });
    }

    private void HandleFailedValidation() {
        this.DoAfterDelay(GameConstants.Instance.kPlayNextSequenceDelay, () => {
            this.PlayCurrentInputSequence();
        });
    }

    [MakeButton]
    private void ValidateCurrentInputSequence() {
        this._sequenceValidator.SetupWithSequence(this.GetCurrentInputSequence());
        this._sequenceValidator.StartValidatingSequence();
    }

    private InputSequence GetCurrentInputSequence() {
        return this.inputSequenceList.GetSequenceForIndex(this._currentIndex);
    }

    private void PlayCurrentInputSequence() {
        if (GameManager.Instance.IsGameFinished()) {
            return;
        }

        this._sequencePlayer.SetupWithSequence(this.GetCurrentInputSequence());
        this._sequencePlayer.PlaySequence();
    }


    // PRAGMA MARK - Serialization
    [MakeButton]
    public void SerializeToTextAsset() {
        if (this._inputSequenceListSource == null) {
            return;
        }

        string serialized = JsonUtility.ToJson(this.inputSequenceList);
#if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(this._inputSequenceListSource);
        File.WriteAllText(Application.dataPath +  assetPath.Replace("Assets", ""), serialized);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }

    [MakeButton]
    public void DeserializeFromTextAsset() {
        if (this._inputSequenceListSource == null) {
            return;
        }

        this.inputSequenceList = JsonUtility.FromJson<InputSequenceList>(this._inputSequenceListSource.text);
    }
}
