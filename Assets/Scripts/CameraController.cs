using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance = 5.0f; // Distance from the player
    public float xSpeed = 120.0f;  // Speed of rotation around the player
    public float ySpeed = 120.0f;  // Speed of vertical rotation

    private float x = 0.0f; // Horizontal angle
    private float y = 0.0f; // Vertical angle

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) // Right mouse button to rotate
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = Mathf.Clamp(y, -20, 80); // Clamp vertical angle
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = player.transform.position - rotation * Vector3.forward * distance;

        transform.rotation = rotation;
        transform.position = position;
    }
}
