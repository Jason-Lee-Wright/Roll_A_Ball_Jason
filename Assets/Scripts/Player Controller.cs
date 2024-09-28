using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;
    public GameObject tipText;
    public Camera mainCamera; // Reference to the main camera

    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        tipText.SetActive(true);

        SetCountText();
        winTextObject.SetActive(false);
        StartCoroutine(UpdateTimer()); // Start the timer coroutine
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
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

    void FixedUpdate()
    {
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * movementY + right * movementX) * Speed;

        rb.AddForce(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}