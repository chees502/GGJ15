using UnityEngine;
using System.Collections;
using System;

public class ModManager : MonoBehaviour {
    static private ModManager _instance;
    const string _instanceName = "ModManager";

    static public ModManager Instance {
        get {
            // If no ModManager instance try to find
            // the Score Manager from the scene
            if (_instance == null) {
                var mmgo = GameObject.Find(_instanceName);
                if (mmgo != null) {
                    _instance = mmgo.GetComponent<ModManager>();
                }
            }

            // If no ModManager instance create a new ModManager
            if (_instance == null) {
                var mmgo = new GameObject(_instanceName);
                var mm = mmgo.AddComponent<ModManager>();
            }

            return _instance;
        }
    }

    private ModInfo[] _effects;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if(_instance != this) {
            Debug.LogWarning("[ModManager]: More then one instance of the script in the scene");
        }

        // Assign which class is to be used for which ModType
        _effects = new ModInfo[Enum.GetNames(typeof(ModType)).Length];
        _effects[(int)ModType.CameraShake]      = new ModCameraShake();
        _effects[(int)ModType.SpeedIncrease]    = new ModSpeedIncrease();
        _effects[(int)ModType.InvertControls]   = new ModInvertControls();
    }

    void Start() {

	}

    void Update() {
        for(int i = 0; i < _effects.Length; i++) {
            _effects[i].Update();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            ModManager.Instance.SetEffect(ModType.CameraShake, true);
        }
    }

    public ModInfo GetEffect(ModType effect) {
        if (_effects != null) {
            return _effects[(int)effect];
        }
        return null;
    }

    public void SetEffect(ModType effect, bool enabled) {
        ModInfo info = GetEffect(effect);
        if (info != null) { 
            info.SetEnabled(enabled);
        }
    }

    public void ToggleEffect(ModType effect) {
        ModInfo info = GetEffect(effect);
        if (info != null) {
            info.Toggle();
        }
    }
}
