using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraJoystick joystick;
    public float speed = 5f;

    void Update()
    {
        Vector3 direction = new Vector3(joystick.inputVector.x, 0, joystick.inputVector.y);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}