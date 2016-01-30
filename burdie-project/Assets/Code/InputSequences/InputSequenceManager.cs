using DT;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomExtensionInspectorAttribute]
public class InputSequenceManager : Singleton<InputSequenceManager> {
    public InputSequenceList inputSequenceList;
    public TextAsset _inputSequenceListSource;

    [Header("Outlets - Set these up!")]
    public InputSequencePlayer _sequencePlayer;
    public InputSequenceValidator _sequenceValidator;

    [MakeButton]
    public void PlayCurrentInputSequence() {
        this._sequencePlayer.SetupWithSequence(this.GetCurrentInputSequence());
        this._sequencePlayer.PlaySequence();
    }

    [MakeButton]
    public void ValidateCurrentInputSequence() {
        this._sequenceValidator.SetupWithSequence(this.GetCurrentInputSequence());
        this._sequenceValidator.StartValidatingSequence();
    }


    // PRAGMA MARK - Internal
    [Header("Properties")]
    [SerializeField]
    private int _currentIndex = 0;

    private void Awake() {
        this.DeserializeFromTextAsset();
    }

    private InputSequence GetCurrentInputSequence() {
        return this.inputSequenceList.GetSequenceForIndex(this._currentIndex);
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
