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
    public InputSequenceList inputSequenceList = new InputSequenceList();

    public TextAsset _inputSequenceListSource;

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
