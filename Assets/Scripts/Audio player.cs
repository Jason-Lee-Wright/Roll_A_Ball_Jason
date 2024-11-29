using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Audioplayer : MonoBehaviour
{
    public PlayableDirector Playable;

    bool hastriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hastriggered)
        {
            if (other.CompareTag("Player"))
            {
                hastriggered = true;
                Playable.Play();
            }
        }
    }
}
