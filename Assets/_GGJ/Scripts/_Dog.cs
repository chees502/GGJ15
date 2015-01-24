using UnityEngine;
using System.Collections;

public class _Dog : MonoBehaviour {
    public static GameObject Dog;
    public static DogCharacterCamera Camera;
    public static DogCharacterController Controller;
    public enum _DogState { Idle, Climbing, Urinating };
    public static _DogState DogState = _DogState.Idle;
	// Use this for initialization
    public static void BuildDogConnection()
    {
        Controller = Dog.GetComponent<DogCharacterController>();
    }
    public static void CallTripping(float time)
    {
        abberationBend.callTripping(time);
    }
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
