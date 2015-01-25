using UnityEngine;
using System.Collections;

public class ScoreTestUI : MonoBehaviour {
    private int score, streak, multiplier;

	void Awake () {
        ScoreManager.OnScoreChange += OnScoreChange;
        ScoreManager.OnStreakChange += OnStreakChange;
        ScoreManager.OnMultiplierChange += OnMultiplierChange;
	}

    void OnScoreChange(int newValue, int oldValue) {
        score = newValue;
    }

    void OnStreakChange(int newValue, int oldValue) {
        streak = newValue;
    }

    void OnMultiplierChange(int newValue, int oldValue) {
        multiplier = newValue;
    }

    void OnGUI() {
        GUI.Box(new Rect(10, 10, 300, 30), string.Format("Score: {0}", score.ToString()));
        GUI.Box(new Rect(10, 40, 300, 30), string.Format("Streak: {0}", streak.ToString()));
        GUI.Box(new Rect(10, 70, 300, 30), string.Format("Multiplier: {0}", multiplier.ToString()));
    }
}
