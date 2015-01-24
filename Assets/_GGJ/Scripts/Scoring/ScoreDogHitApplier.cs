using UnityEngine;
using System.Collections;

public class ScoreDogHitApplier : MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        var otherHit = other.gameObject.GetComponent<ScoreDogHitObject>();
        if (otherHit != null) {
            otherHit.Refresh();
        } else {
            other.gameObject.AddComponent<ScoreDogHitObject>();
            //Debug.Log("[ScoreDogHitApplier]: Appling ScoreDogHitObject");
        }
    }
}
