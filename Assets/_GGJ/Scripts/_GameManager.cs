using UnityEngine;
using System.Collections;

public class _GameManager : MonoBehaviour {
    public enum _gameMode {None ,ScoreAttack, FreeRoam }
    public static _gameMode gameMode = _gameMode.None;
    public static Bounds SceneBound=new Bounds(Vector3.zero,new Vector3(40,20,40));
    public static bool isOculus = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
