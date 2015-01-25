using UnityEngine;
using System.Collections;

public class _Dog {
    public enum _DogState { 
        Idle,       // Basic Movement
        Climbing,   // Climbin Objects
        Urinating,  // Urin CANNON!
        ActionLock  // Locks character for external handling
    };

    public delegate void DogStateEvent(_DogState newState);

    public static event DogStateEvent OnDogStateChange;

    private static GameObject _dog;
    private static DogCharacterCamera _camera;
    private static DogCharacterController _controller;
    private static _DogState _dogState = _DogState.Idle;

    public static Maw maw;

    public static GameObject Dog
    {
        get { return _dog; }
        set { _dog = value; }
    }

    public static DogCharacterCamera Camera
    {
        get { return _camera; }
        set { _camera = value; }
    }

    public static DogCharacterController Controller
    {
        get { return _controller; }
        set { _controller = value; }
    }

    public static _DogState DogState
    {
        get { return _dogState; }
        set
        {
            if (value != _dogState)
            {
                TriggerDogStateChangeEvent(value);
            }
            _dogState = value;
        }
    }

    private static void TriggerDogStateChangeEvent(_DogState newState) {
        if(OnDogStateChange != null) {
            OnDogStateChange(newState);
        }
    }

	// Use this for initialization
    public static void BuildDogConnection()
    {
        Controller = Dog.GetComponent<DogCharacterController>();
    }
    public static void CallTripping(float time)
    {
        abberationBend.callTripping(time);
    }
}
