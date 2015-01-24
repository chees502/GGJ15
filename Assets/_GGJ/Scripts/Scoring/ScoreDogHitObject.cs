using UnityEngine;
using System.Collections;

public class ScoreDogHitObject : MonoBehaviour {
    public float durationActive = 4f;
    private float _duration = 0f;

    public void Refresh() {
        _duration = durationActive;
    }

    void Awake() {
        Refresh();
    }

    void Update() {
        _duration -= Time.deltaTime;
        if (_duration <= 0) {
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player") {
            var otherHit = other.gameObject.GetComponent<ScoreDogHitObject>();
            if (otherHit == null) {
                other.gameObject.AddComponent<ScoreDogHitObject>();
                //Debug.Log("[ScoreDogHitObject]: Appling ScoreDogHitObject");
            }
        }
    }
}
