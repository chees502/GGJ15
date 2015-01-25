using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    int w;
    int h;
    int hw;
    int hh;
    public enum _menuState { MainMenu, Credits, Options };
    public _menuState menuState = _menuState.MainMenu;
	// Use this for initialization
	void Start () {

        w = Screen.width;
        h = Screen.height;
        hw = Mathf.FloorToInt(w * 0.5f);
        hh = Mathf.FloorToInt(h * 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

        w = Screen.width;
        h = Screen.height;
        hw = Mathf.FloorToInt(w * 0.5f);
        hh = Mathf.FloorToInt(h * 0.5f);
	}
    void OnGUI()
    {
        switch (menuState)
        {
            case _menuState.MainMenu:
                Main();
                break;
            case _menuState.Credits:
                Credits();
                break;
            case _menuState.Options:
                Options();
                break;
        }
    }
    void Main()
    {
        GUI.Label(new Rect(hw - 50, 100, hw + 50, 50), "Pugs and Thugs");
        if (GUI.Button(new Rect(hw - 50, h - 100, 100, 50), "Play"))
        {
            Application.LoadLevel("DebugScene");
        }
        if (GUI.Button(new Rect(w - 125, h - 100, 100, 50), "Credits"))
        {
            menuState = _menuState.Credits;
        }
        if (GUI.Button(new Rect(w - 100, 25, 50, 50), "☼"))
        {
            menuState = _menuState.Options;
        }
    }
    void Credits()
    {
        if (GUI.Button(new Rect(25, 25, 50, 50), "◄"))
        {
            menuState = _menuState.MainMenu;
        }
    }
    void Options()
    {
        if (GUI.Button(new Rect(25, 25, 50, 50), "◄"))
        {
            menuState = _menuState.MainMenu;
        }
    }
}
