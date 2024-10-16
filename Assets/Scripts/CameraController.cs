using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance = 5.0f; 
    public float xSpeed = 120.0f;  
    public float ySpeed = 120.0f;

    private float x = 0.0f; 
    private float y = 0.0f; 

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = Mathf.Clamp(y, -20, 80); 
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = player.transform.position - rotation * Vector3.forward * distance;

        transform.rotation = rotation;
        transform.position = position;
    }
}
