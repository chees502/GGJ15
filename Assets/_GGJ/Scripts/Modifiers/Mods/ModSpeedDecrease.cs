using UnityEngine;
using System.Collections;

public class ModSpeedDecrease : ModInfo {
    public float speedMultiplier = 0.5f;

    private float horizontalAccelDefault;
    private float verticalAccelDefault;
    private float maxForwardRightVelDefault;

    public ModSpeedDecrease()
        : base(ModType.SpeedDecrease) {
        
    }

    public override void OnEnabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.speedMultiplier = speedMultiplier;
        }
        ModManager.Instance.SetEffect(ModType.SpeedIncrease, false);
    }

    public override void OnDisabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.speedMultiplier = 1.0f;
        }
    }

    public override void OnStay() {

    }
}
