using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    static private ScoreManager _instance;
    static public ScoreManager Instance {
        get {
            // If no ScoreManager instance try to find
            // the Score Manager from the scene
            if (_instance == null) {
                var smgo = GameObject.Find("ScoreManager");
                if(smgo != null) {
                    _instance = smgo.GetComponent<ScoreManager>();
                }
            }

            // If no ScoreManager instance create a new ScoreManager
            if (_instance == null) {
                var smgo = new GameObject("ScoreManager");
                var sm = smgo.AddComponent<ScoreManager>();
            }

            return _instance; 
        }
    }

    public delegate void IntValueChangeEvent(int newValue, int oldValue);
    public static event IntValueChangeEvent OnScoreChange;
    public static event IntValueChangeEvent OnStreakChange;
    public static event IntValueChangeEvent OnMultiplierChange;

    private int _score;
    private int _streak;
    private int _multiplier;
    private float _streakDuration;


    public float maxStreakDuration = 4f;
    public int itemsPerMultiplier = 5;

    public int Score {
        get { return _score; }
    }

    public int Streak {
        get { return _streak; }
    }

    public int Multiplier {
        get { return _multiplier; }
    }

    public float StreakDuration {
        get { return _streakDuration; }
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

    void Update() {
        if (Streak > 0) {
            _streakDuration -= Time.deltaTime;
            if (_streakDuration <= 0f) {
                ClearStreak();
            }
        }
    }

    public void AddScore(int value) {
        if (value > 0) {
            SetScore(_score + (value * Multiplier));
            SetStreak(_streak + 1);
            SetMultiplier((int)Mathf.Floor(_streak / itemsPerMultiplier) + 1);
            _streakDuration = maxStreakDuration;
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
            //Debug.Log(string.Format("[OnScoreChange]: (New Value) {0}, (Old Value) {1}", value, _score));
            if (OnScoreChange != null) {
                OnScoreChange(value, _score);
            }
            _score = value;
        }
    }

    void SetStreak(int value) {
        if (value != _streak) {
            //Debug.Log(string.Format("[OnStreakChange]: (New Value) {0}, (Old Value) {1}", value, _streak));
            if (OnStreakChange != null) {
                OnStreakChange(value, _streak);
            }
            _streak = value;
        }
    }

    void SetMultiplier(int value) {
        if (value != _multiplier) {
            //Debug.Log(string.Format("[OnMultiplierChange]: (New Value) {0}, (Old Value) {1}", value, _multiplier));
            if (OnMultiplierChange != null) {
                OnMultiplierChange(value, _multiplier);
            }
            _multiplier = value;
        }
    }
}
