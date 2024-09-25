using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        SetTimeCount();
    }

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
