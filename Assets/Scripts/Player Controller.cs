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
    public float speed = 0;
    public float MaxVelosity = 5;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;
    public GameObject mainCamera;
    public Vector3 Spawn;


    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Spawn = GameObject.Find("Player").transform.position;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        StartCoroutine(UpdateTimer());
    }

    void PlayerResetComponent()
    {
        if (transform.position.y <= -10)
        {
            this.transform.position = Spawn;
        }
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

        PlayerResetComponent();

        if (rb.velocity.sqrMagnitude > MaxVelosity)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            rb.velocity *= 0.99f;
        }
    }

    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            Time.timeScale = 0;

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
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
        countText.text = "Count: " + count.ToString() + " / 4";

       if (count >= 4)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
           
            winTextObject.SetActive(true);

            Time.timeScale = 0;
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
