using UnityEngine;
using System.Collections;

public class ModVisualTripping : ModInfo {
    public ModVisualTripping() : base(ModType.VisualTripping) {}

    public override void OnEnabled() {
        abberationBend.Instance.StartTrip(duration);
        //ModManager.Instance.SetEffect(ModType.VisualHot, false);
    }

    public override void OnDisabled() {
        abberationBend.Instance.CancelTrip();
    }

    public override void OnStay() {

    }
}
