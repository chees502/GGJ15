using UnityEngine;
using System.Collections;

public class UserInterfaceScript : MonoBehaviour {

    public GUIText Timer;
    public float timeLeft;

    void Update() {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            //Game over
        }
        else {
            int minutes = (int)timeLeft / 60;
            int seconds = (int)timeLeft % 60;
            Timer.text = minutes + ":" + seconds;
        }
    }
}
