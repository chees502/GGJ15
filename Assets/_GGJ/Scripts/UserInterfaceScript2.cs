using UnityEngine;
using System.Collections;

public class UserInterfaceScript2 : MonoBehaviour {

    public GameObject TimerObject;
    public GameObject Icon1, Icon2, Icon3, Icon4;
    public float timeLeft = 180.0f;

    void FormatTime(float time) {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        TimerObject.guiText.text = minutes + ":" + seconds;
    }

    void Countdown() {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            //Game over

        }
        else
        {
            FormatTime(timeLeft);            
        }
    }

    //On Start
    void Awake() {
        //Create Timer Object
        TimerObject = new GameObject("Timer");
        TimerObject.transform.position = new Vector3(0.5f, 0.98f, 0.5f);
        TimerObject.AddComponent("GUIText");
        TimerObject.guiText.anchor = TextAnchor.UpperCenter;
        TimerObject.guiText.alignment = TextAlignment.Center;
        TimerObject.guiText.fontSize = 20;
        FormatTime(timeLeft);
        //Create Icons
        //Icon 01
        Icon1 = new GameObject("Icon01");
        Icon1.transform.position = new Vector3(0.025f, 0.915f, 0.0f);
        Icon1.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon1.AddComponent("GUITexture");
        Icon1.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder1") as Texture;
        Icon1.guiTexture.pixelInset = new Rect(0.0f, -50.0f, 100.0f, 100.0f);
        //Icon 02
        Icon2 = new GameObject("Icon02");
        Icon2.transform.position = new Vector3(0.125f, 0.9475f, 0.0f);
        Icon2.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon2.AddComponent("GUITexture");
        Icon2.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder2") as Texture;
        Icon2.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        //Icon 03
        Icon3 = new GameObject("Icon03");
        Icon3.transform.position = new Vector3(0.175f, 0.9475f, 0.0f);
        Icon3.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon3.AddComponent("GUITexture");
        Icon3.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder3") as Texture;
        Icon3.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        //Icon 04
        Icon4 = new GameObject("Icon04");
        Icon4.transform.position = new Vector3(0.175f, 0.88125f, 0.0f);
        Icon4.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon4.AddComponent("GUITexture");
        Icon4.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder4") as Texture;
        Icon4.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
    }

    //On Frame
    void Update() {
        Countdown();
    }
}
