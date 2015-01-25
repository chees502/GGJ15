using UnityEngine;
using System.Collections;

public class ModCameraShake : ModInfo {
    public GameObject cameraShakeGO;
    public Shake cameraShake;

    public ModCameraShake() : base(ModType.CameraShake) {
        ValidateGameObjects();
    }

    public void ValidateGameObjects() {
        if (cameraShakeGO == null) {
            cameraShakeGO = GameObject.Find("CameraShake");
            if (cameraShakeGO == null) {
                Debug.LogWarning("[ModCameraShake]: No GameObject named \"CameraShake\" in scene");
            }
        }

        if (cameraShake == null && cameraShakeGO != null) {
            cameraShake = cameraShakeGO.GetComponent<Shake>();
            if (cameraShake == null) {
                Debug.LogWarning("[ModCameraShake]: No Shake script attached to \"CameraShake\" GameObject");
            }
        } 
    }

    public override void OnEnabled() {
        ValidateGameObjects();
        Debug.Log("Enabled Camera Shake");
        cameraShake.TogglePosShake(true, true, false);
        cameraShake.ToggleRotShake(true, true, false);
    }

    public override void OnDisabled() {
        ValidateGameObjects();
        cameraShake.TurnShakeOff();
    }

    public override void OnStay() {
        cameraShake.SetDamping(Mathf.Clamp(currentDuration, 0f, 0.4f));
    }
}
