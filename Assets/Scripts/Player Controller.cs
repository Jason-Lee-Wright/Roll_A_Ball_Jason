using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;
    public GameObject TipText;
    public GameObject mainCamera;


    private float movementX;
    private float movementY;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        TipText.SetActive(true);
        StartCoroutine(UpdateTimer());
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void Update()
    {
        float ElapsedTime = Time.timeSinceLevelLoad;

        if (ElapsedTime > 2)
        {
            TipText.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Get the camera's forward and right directions
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Make the movement flat on the ground
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to avoid faster diagonal movement
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create the movement direction based on camera orientation
        Vector3 movement = cameraForward * movementY + cameraRight * movementX;

        // Apply force to the Rigidbody
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("PickUp"))
         {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();
         }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

       if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    IEnumerator UpdateTimer()
   
    {
        while (!winTextObject.activeSelf)
        {
            timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
            yield return null;
        }

        timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
    }

}
