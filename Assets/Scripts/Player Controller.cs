using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using System;
using UnityEditorInternal;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;

    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

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
        while (!winTextObject.activeSelf) // Check if winTextObject is active
        {
            timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
            yield return null; // Wait for the next frame
        }

        // Final update when the game is over
        timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * Speed);
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