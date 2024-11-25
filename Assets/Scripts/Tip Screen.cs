using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipScreen : MonoBehaviour
{
    public GameObject Tip;

    public void Start()
    {
        Time.timeScale = 0;
    }

    public void LeaveMenu()
    {
        Time.timeScale = 1;
        Tip.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
            Tip.SetActive(true);
        }
    }
}
