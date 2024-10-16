using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool isPaused = false;

    private void Start()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1; // Ensure the game is running at normal speed
    }

    private void Update() // Changed to Update for input detection
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0; // Stop the game time
    }

    public void Continue()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game time
    }

    public void ToMain()
    {
        Time.timeScale = 1; // Ensure time resumes when going to the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}