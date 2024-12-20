using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float MaxVelosity = 5;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timeText;
    public GameObject mainCamera;
    public Vector3 Spawn;
    public GameObject cavedoor;
    public PlayableDirector endscene;
    public GameObject cam;
    public GameObject car;


    float Walking;
    float Running;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private void Awake()
    {
        cam.SetActive(false);
        car.SetActive(false);
    }

    void Start()
    {

        Walking = MaxVelosity;
        Running = MaxVelosity * 2;

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
            rb.velocity = Vector3.zero;
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MaxVelosity = Running;
            speed = speed * 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MaxVelosity = Walking;
            speed = speed / 5;
        }


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

    void Show()
    {
        winTextObject.GetComponent<TextMeshProUGUI>().text = "A.. cave? \nhas opened";
        winTextObject.SetActive(true);
        Invoke("Hide", 4.0f);
    }
    void Hide()
    {
        winTextObject.SetActive(false);
    }

    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / 4";

        if (count == 3)
        {
            cavedoor.SetActive(false);
            Invoke("Show", 1.5f);
        }

        if (count >= 4)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all enemies
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy); // Destroy each enemy
            }

            winTextObject.GetComponent<TextMeshProUGUI>().text = "Something is happening...";
            winTextObject.SetActive(true);

            Cursor.visible = false;

            Invoke("Hide", 2.0f);
            Invoke("PlayCutScene", 3.0f);
            Invoke("EndGame", 46.50f);
        }
    }

    void PlayCutScene()
    {
        Time.timeScale = 1;
        endscene.Play();
    }

    IEnumerator UpdateTimer()
   
    {
        while (!winTextObject.activeSelf && count < 4)
        {
            timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
            yield return null;
        }

        timeText.text = $"Time: {Math.Round(Time.timeSinceLevelLoad, 0)}";
    }

}
