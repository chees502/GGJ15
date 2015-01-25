using UnityEngine;
using System.Collections;

public class ModInfo {
    public delegate void EffectEvent(ModType type, ModInfo info);
    public event EffectEvent OnEffectEnabled;       // On Enabled
    public event EffectEvent OnEffectDisabled;      // On Disabled
    public event EffectEvent OnEffectStay;          // While Effect is on

    protected void TriggerEffectEnabled(ModType type, ModInfo info) {
        Debug.Log(string.Format("[ModManager][OnEffectEnabled]: (EffectType) {0} enabled", type.ToString()));
        if (OnEffectEnabled != null) {
            OnEffectEnabled(type, info);
        }
    }

    protected void TriggerEffectDisabled(ModType type, ModInfo info) {
        Debug.Log(string.Format("[ModManager][OnEffectDisabled]: (EffectType) {0} disabled", type.ToString()));
        if (OnEffectDisabled != null) {
            OnEffectDisabled(type, info);
        }
    }

    protected void TriggerEffectStay(ModType type, ModInfo info) {
        Debug.Log(string.Format("[ModManager][OnEffectStay]: (EffectType) {0} stay", type.ToString()));
        if (OnEffectStay != null) {
            OnEffectStay(type, info);
        }
    }

    public bool enabled;
    public float duration;
    public float currentDuration;
    public ModType type;

    public virtual void Update() {
        if (enabled) {
            if (currentDuration > 0f) {
                currentDuration -= Time.deltaTime;
            } else {
                currentDuration = duration;
                enabled = false;
            }

            TriggerEffectStay(type, this);
        }
    }

    public virtual void Toggle() {
        Set(!enabled);
        if (enabled) {
            TriggerEffectEnabled(type, this);
        } else {
            TriggerEffectDisabled(type, this);
        }
    }

    public virtual void Set(bool enabled) {
        if (this.enabled != enabled) {
            TriggerEffectEnabled(type, this);
        } else {
            TriggerEffectDisabled(type, this);
        }
        this.enabled = enabled;
        this.currentDuration = duration;
    }
}
