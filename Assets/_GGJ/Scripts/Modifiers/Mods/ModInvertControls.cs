using UnityEngine;
using System.Collections;

public class ModInvertControls : ModInfo {

    public ModInvertControls() : base(ModType.InvertControls) {
        
    }

    public override void OnEnabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.invertControls = true;
        }
    }

    public override void OnDisabled() {
        if (DogCharacterController.Instance != null) {
            DogCharacterController.Instance.invertControls = false;
        }
    }

    public override void OnStay() {

    }
}

