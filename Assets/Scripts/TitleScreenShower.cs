using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenShower : MonoBehaviour
{
    public GameObject abstarction;
    public GameObject every;
    // Start is called before the first frame update
    void Start()
    {
        abstarction.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        abstarction.SetActive(true);
        every.SetActive(false);
    }
}
