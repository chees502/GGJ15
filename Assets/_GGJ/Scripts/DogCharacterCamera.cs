using UnityEngine;
using System.Collections;

public class DogCharacterCamera : MonoBehaviour {
    public float cameraLookRate = 10f;
    public Transform followTarget;
	
	// Update is called once per frame
	void Update () {
        transform.position = followTarget.position;
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraLookRate * Time.deltaTime);
	}
}
