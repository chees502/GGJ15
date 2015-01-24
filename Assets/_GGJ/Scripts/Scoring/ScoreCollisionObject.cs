using UnityEngine;
using System.Collections;

public class ScoreCollisionObject : MonoBehaviour {
    public int minScore = 1;
    public int maxScore = 1;

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

    void GiveScore() {
        Debug.Log("Giving Score");
        ScoreManager.Instance.AddScore(Random.Range(minScore, maxScore));
        Destroy(this);
    }
}
