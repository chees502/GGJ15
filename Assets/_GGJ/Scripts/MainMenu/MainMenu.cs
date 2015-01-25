using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    int w;
    int h;
    int hw;
    int hh;
    public enum _menuState { MainMenu, Credits, Options };
    public _menuState menuState = _menuState.MainMenu;
    public Renderer menuItemTexture;
    public Texture2D[] menus;
    public Camera creditCam;
    private int _currentState = 0;
    public int currentState
    {
        get { return _currentState; }
        set {
            if (_currentState != value)
            {
                _currentState = value;
                updateTexture();
                Debug.Log("Change to " + value);
            }
            else
            {
                _currentState = value;
            }
        }
    }
	// Use this for initialization
	void Start () {

        w = Screen.width;
        h = Screen.height;
        hw = Mathf.FloorToInt(w * 0.5f);
        hh = Mathf.FloorToInt(h * 0.5f);
	}
    void updateTexture()
    {
        if (currentState != -1)
        {
            menuItemTexture.material.SetTexture("_MainTex", menus[_currentState]);
        }
        else
        {
            menuItemTexture.material.SetTexture("_MainTex", menus[5]);

        }
    }
	// Update is called once per frame
	void Update () {

        w = Screen.width;
        h = Screen.height;
        hw = Mathf.FloorToInt(w * 0.5f);
        hh = Mathf.FloorToInt(h * 0.5f);
        if (menuState != _menuState.MainMenu)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
        {
            ButtonLogic button = hit.collider.transform.gameObject.GetComponent<ButtonLogic>();
            if (button)
            {
                currentState = button.state;
            }
        }
        else
        {
            currentState = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentState)
            {
                case 1:
                    _GameManager.gameMode = _GameManager._gameMode.ScoreAttack;
                    Application.LoadLevel("DebugScene");
                    break;
                case 2:
                    _GameManager.gameMode = _GameManager._gameMode.ScoreAttack;
                    Application.LoadLevel("DebugScene");
                    break;
                case 3:
                    menuState = _menuState.Credits;
                    currentState = 5;
                    creditCam.enabled = true;
                    break;
                case 4:
                    Application.CancelQuit();
                    break;


            }
        }
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
    {/*
        if (GUI.Button(new Rect(hw - 25, h - 100, 50, 50), "Free\nStrut"))
        {
            _GameManager.gameMode = _GameManager._gameMode.FreeRoam;
            Application.LoadLevel("DebugScene");
        }
        if (GUI.Button(new Rect(hw - 75, h - 100, 50, 50), "Score\nAttack"))
        {
            _GameManager.gameMode = _GameManager._gameMode.ScoreAttack;
            Application.LoadLevel("DebugScene");
        }
        if (GUI.Button(new Rect(w - 125, h - 100, 100, 50), "Credits"))
        {
            menuState = _menuState.Credits;
        }
        if (GUI.Button(new Rect(w - 100, 25, 50, 50), "☼"))
        {
            menuState = _menuState.Options;
        }*/
    }
    void Credits()
    {
        if (GUI.Button(new Rect(25, 25, 50, 50), "◄"))
        {
            menuState = _menuState.MainMenu;
            currentState = 0;
            creditCam.enabled = false;
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
