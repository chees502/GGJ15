using UnityEngine;
using System.Collections;

public class ModVisualHot : ModInfo {
    public ModVisualHot() : base(ModType.VisualHot) {}

    public override void OnEnabled() {
        abberationBend.StartAllHot(duration);
        //ModManager.Instance.SetEffect(ModType.VisualTripping, false);
    }

    public override void OnDisabled() {
        abberationBend.CancelAllHot();
    }

    public override void OnStay() {

    }
}
