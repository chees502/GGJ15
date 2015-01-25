using UnityEngine;
using System.Collections;

public class ModVisualTripping : ModInfo {
    public ModVisualTripping() : base(ModType.VisualTripping) {}

    public override void OnEnabled() {
        abberationBend.StartAllTripping(duration);
        //ModManager.Instance.SetEffect(ModType.VisualHot, false);
    }

    public override void OnDisabled() {
        abberationBend.CancelAllTripping();
    }

    public override void OnStay() {

    }
}
