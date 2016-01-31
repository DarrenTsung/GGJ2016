using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppInit : MonoBehaviour {
    private void Start() {
        AkSoundEngine.PostEvent("Play_Music", null);
    }
}
