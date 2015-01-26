using UnityEngine;
using System.Collections;

public class _GameManager : MonoBehaviour {
    public enum _gameMode {None ,ScoreAttack, FreeRoam }
    public static _gameMode gameMode = _gameMode.None;
    public static Bounds SceneBound=new Bounds(new Vector3(1120.0f, 100.0f, 1120.0f),new Vector3(2140,200,2140));
    public static bool isOculus = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
