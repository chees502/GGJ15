using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour {
    public ParticleSystem onUseParticle;
    public AudioSource onUseAudio;

    [System.Serializable]
    public class Modifier {
        public ModType type;
        public float duration;
    }

    public Modifier[] onUseModEnabled;
    

    void OnTriggerEnter(Collider other) {
        if (onUseParticle != null) {
            onUseParticle.Play();
        }

        if (onUseAudio != null) {
            onUseAudio.Play();
        }

        foreach (Modifier mod in onUseModEnabled) {
            ModInfo info = ModManager.Instance.GetEffect(mod.type);
            info.SetEnabled(true);
            info.SetDuration(mod.duration);
        }
    }
}
