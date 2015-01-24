using UnityEngine;
using System.Collections;

public class DogCharacterCamera : MonoBehaviour {
    public float cameraLookRate = 10f;

    void Awake()
    {
        _Dog.Camera = this;
    }
	void Update () {
        transform.position = _Dog.Dog.transform.position;
        
	}
}
