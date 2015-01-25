using UnityEngine;
using System.Collections;

[RequireComponent(typeof(asset_deform))]
public class ScoreCollisionObject : MonoBehaviour {
    public int minHitScore = 1;
    public int maxHitScore = 1;

    public int minDestroyScore = 1;
    public int maxDestroyScore = 1;

    public asset_deform assetDeform;

    void Awake() {
        assetDeform = GetComponent<asset_deform>();
        if (assetDeform == null) {
            assetDeform = gameObject.AddComponent<asset_deform>();
        }

        assetDeform.OnObjectDamage += OnObjectDamaged;
        assetDeform.OnObjectDestroy += OnObjectDestroyed;
    }

    void OnObjectDamaged() {
        GiveScore(minHitScore, maxHitScore);
    }

    void OnObjectDestroyed() {
        GiveScore(minDestroyScore, maxDestroyScore);
    }

    /*
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            GiveScore();
        } else {
            ScoreDogHitObject otherHit = other.gameObject.GetComponent<ScoreDogHitObject>();
            if (otherHit != null) {
                Debug.Log(this.gameObject.name + " Collided with " + other.gameObject.name);
                GiveScore();
            }
        }
    }
     */

    void GiveScore(int min, int max) {
        Debug.Log("Giving Score");
        ScoreManager.Instance.AddScore(Random.Range(min, max));
        Destroy(this);
    }
}
