using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private int time;


    void SetTimeCount()
    {
        timeText.text = "Count: " + time.ToString();
        while (true)
        {
            time++;

        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
