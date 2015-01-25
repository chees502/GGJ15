using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour {
    public ParticleSystem onUseParticle;
    public AudioSource onUseAudio;
    public ModType onUseModEnabled;

    void OnTriggerEnter(Collider other) {
        onUseParticle.Play();
        onUseAudio.Play();
        ModManager.Instance.SetEffect(onUseModEnabled, true);
    }
}
