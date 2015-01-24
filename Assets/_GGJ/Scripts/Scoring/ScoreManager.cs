using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    static private ScoreManager _instance;
    static public ScoreManager Instance {
        get { return _instance; }
    }

    public delegate void IntValueChangeEvent(int newValue, int oldValue);
    public event IntValueChangeEvent OnScoreChange;
    public event IntValueChangeEvent OnStreakChange;
    public event IntValueChangeEvent OnMultiplierChange;

    public int _score;
    public int _streak;
    public int _multiplier;

    public int ItemsPerMultiplier = 5;

    public int Score {
        get { return _score; }
    }

    public int Streak {
        get { return _streak; }
    }

    public int Multiplier {
        get { return _multiplier; }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Debug.LogWarning("[ScoreManager]: More then one instance of the script in the scene");
        }

        _score = 0;
        _streak = 0;
        _multiplier = 1;
    }

    public void AddScore(int value) {
        if (value > 0) {
            SetScore(_score + value);
            SetStreak(_streak + 1);
            SetMultiplier((int)Mathf.Floor(_streak / ItemsPerMultiplier) + 1);
        }
    }

    public void ClearStreak() {
        SetStreak(0);
        SetMultiplier(1);
    }

    public void ClearScore() {
        SetScore(0);
        SetStreak(0);
        SetMultiplier(1);
    }

    void SetScore(int value) {
        if (value != _score) {
            Debug.Log(string.Format("[OnScoreChange]: (New Value) {0}, (Old Value) {1}", value, _score));
            if (OnScoreChange != null) {
                OnScoreChange(value, _score);
            }
            _score = value;
        }
    }

    void SetStreak(int value) {
        if (value != _streak) {
            Debug.Log(string.Format("[OnStreakChange]: (New Value) {0}, (Old Value) {1}", value, _streak));
            if (OnStreakChange != null) {
                OnStreakChange(value, _streak);
            }
            _streak = value;
        }
    }

    void SetMultiplier(int value) {
        if (value != _multiplier) {
            Debug.Log(string.Format("[OnMultiplierChange]: (New Value) {0}, (Old Value) {1}", value, _multiplier));
            if (OnMultiplierChange != null) {
                OnMultiplierChange(value, _multiplier);
            }
            _multiplier = value;
        }
    }
}
