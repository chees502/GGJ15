using UnityEngine;
using System.Collections;

/// <summary>
/// Shakes the the gameobject that this script is attached to on
/// either its position and rotation.
/// 
/// This script goes off the idea that it is the child of another 
/// gameobject and always has a local position of zero 
/// and rotation of zero
/// </summary>
public class Shake : MonoBehaviour {
    public float posRadius;
    public float posLerpRate;
    public float posUpdateRate;
    public bool posShakeX, posShakeY, posShakeZ;

    public float rotMax;
    public float rotLerpRate;
    public float rotUpdateRate;
    public bool rotShakeX, rotShakeY, rotShakeZ;

    private Vector3 _targetPosition;
    private Vector3 _targetRotation;
    private float _nextPositionUpdate;
    private float _nextRotationUpdate;

	void Start () {
        _targetPosition = GetNewTargetPos();
        _targetRotation = GetNewTargetRot();
        _nextPositionUpdate = posUpdateRate;
        _nextRotationUpdate = rotUpdateRate;
	}
	
	void Update () {
        if (posShakeX || posShakeY || posShakeZ) {
            if (_nextPositionUpdate > 0f) {
                _nextPositionUpdate -= Time.deltaTime;
            } else {
                ForceUpdate(true, false);
            }

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                _targetPosition,
                posLerpRate * Time.deltaTime);
        }

        if (rotShakeX || rotShakeY || rotShakeZ) {
            if (_nextRotationUpdate > 0f) {
                _nextRotationUpdate -= Time.deltaTime;
            } else {
                ForceUpdate(false, true);
            }

            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.Euler(_targetRotation),
                rotLerpRate * Time.deltaTime);
        }
	}

    public void TogglePosShake(bool x, bool y, bool z) {
        posShakeX = x;
        posShakeY = y;
        posShakeZ = z;
    }

    public void ToggleRotShake(bool x, bool y, bool z) {
        rotShakeX = x;
        rotShakeY = y;
        rotShakeZ = z;
    }

    public void TurnShakeOff() {
        TogglePosShake(false, false, false);
        ToggleRotShake(false, false, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void ForceUpdate(bool positon, bool rotation) {
        if (positon) {
            _targetPosition = GetNewTargetPos();
            _nextPositionUpdate = posUpdateRate;
        }
        if (rotation) {
            _targetRotation = GetNewTargetRot();
            _nextRotationUpdate = rotUpdateRate;
        }
    }

    Vector3 GetNewTargetPos() {
        return new Vector3(
            posShakeX ? Random.Range(-posRadius, posRadius) : 0f,
            posShakeY ? Random.Range(-posRadius, posRadius) : 0f,
            posShakeZ ? Random.Range(-posRadius, posRadius) : 0f);
    }

    Vector3 GetNewTargetRot() {
        return new Vector3(
            rotShakeX ? Random.Range(-rotMax, rotMax) : 0f,
            rotShakeY ? Random.Range(-rotMax, rotMax) : 0f,
            rotShakeZ ? Random.Range(-rotMax, rotMax) : 0f);
    }
}
