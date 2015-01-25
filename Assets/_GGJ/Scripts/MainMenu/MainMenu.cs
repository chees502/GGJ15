using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    int w;
    int h;
    int hw;
    int hh;
	// Use this for initialization
	void Start () {

        w = Screen.width;
        h = Screen.height;
        hw = Mathf.FloorToInt(w * 0.5f);
        hh = Mathf.FloorToInt(h * 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        GUI.Label(new Rect(hw - 50, 100, hw + 50, 50), "Pugs and Thugs");
        if(GUI.Button(new Rect(hw-50,h-100,hw+50,h-50),"Play")){

        }
    }
}
