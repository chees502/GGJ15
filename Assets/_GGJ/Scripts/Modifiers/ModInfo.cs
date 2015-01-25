using UnityEngine;
using System.Collections;

public class ModInfo {
    public delegate void EffectEvent(ModType type, ModInfo info);
    public event EffectEvent OnEffectEnabled;       // On Enabled
    public event EffectEvent OnEffectDisabled;      // On Disabled
    public event EffectEvent OnEffectStay;          // While Effect is on

    protected void TriggerEffectEnabled(ModType type, ModInfo info) {
        Debug.Log(string.Format("[ModManager][OnEffectEnabled]: (EffectType) {0} enabled", type.ToString()));
        OnEnabled();
        if (OnEffectEnabled != null) {
            OnEffectEnabled(type, info);
        }
    }

    protected void TriggerEffectDisabled(ModType type, ModInfo info) {
        Debug.Log(string.Format("[ModManager][OnEffectDisabled]: (EffectType) {0} disabled", type.ToString()));
        OnDisabled();
        if (OnEffectDisabled != null) {
            OnEffectDisabled(type, info);
        }
    }

    protected void TriggerEffectStay(ModType type, ModInfo info) {
        //Debug.Log(string.Format("[ModManager][OnEffectStay]: (EffectType) {0} stay", type.ToString()));
        OnStay();
        if (OnEffectStay != null) {
            OnEffectStay(type, info);
        }
    }

    public bool enabled = false;
    public float duration = 0;
    public float currentDuration = 0;
    public ModType type;

    public ModInfo() {
        this.enabled = false;
        this.duration = 0;
        this.currentDuration = 0;
        this.type = 0;
    }

    public ModInfo(ModType type) {
        this.enabled = false;
        this.duration = 0;
        this.currentDuration = 0;
        this.type = type;
    }

    public virtual void OnEnabled() { }
    public virtual void OnDisabled() { }
    public virtual void OnStay() { }

    public void Update() {
        if (enabled) {
            TriggerEffectStay(type, this);
            if (currentDuration > 0f) {
                currentDuration -= Time.deltaTime;
            } else {
                currentDuration = duration;
                enabled = false;
                Debug.Log("1");
                TriggerEffectDisabled(type, this);
            }
        }
    }

    public void Toggle() {
        SetEnabled(!enabled);
        Debug.Log("2");
        if (enabled) {
            TriggerEffectEnabled(type, this);
        } else {
            TriggerEffectDisabled(type, this);
        }
    }

    public void SetEnabled(bool enabled) {
        if (this.enabled != enabled) {
            if (enabled) {
                TriggerEffectEnabled(type, this);
            } else {
                TriggerEffectDisabled(type, this);
            }
        }
        this.enabled = enabled;
        this.currentDuration = duration;
    }

    public void Set(bool enabled, float duration) {
        SetEnabled(enabled);
        SetDuration(duration);
    }

    public void SetDuration(float duration) {
        if (this.currentDuration < duration) {
            this.duration = duration;
            this.currentDuration = duration;
        }
    }
}
