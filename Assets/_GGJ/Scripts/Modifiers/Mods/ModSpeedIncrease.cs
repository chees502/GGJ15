using UnityEngine;
using System.Collections;

public class ModSpeedIncrease : ModInfo {
    public float speedMultiplier = 2.0f;

    private float horizontalAccelDefault;
    private float verticalAccelDefault;
    private float maxForwardRightVelDefault;

    public ModSpeedIncrease() : base(ModType.SpeedIncrease) {
        
    }

    public override void OnEnabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.speedMultiplier = speedMultiplier;
        }

        ModManager.Instance.SetEffect(ModType.SpeedDecrease, false);
    }

    public override void OnDisabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.speedMultiplier = 1.0f;
        }
    }

    public override void OnStay() {

    }
}
