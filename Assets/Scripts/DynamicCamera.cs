using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float angularSpeed = 1;
    public CameraState state = CameraState.STOPPED;
    private Transform cameraPosition;

    public void LookAt(Transform target) { }
    public void SetPosition(Transform point) { }

}

public enum CameraState { STOPPED, TAKING_POSITION}