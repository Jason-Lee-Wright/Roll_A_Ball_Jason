using System.Collections;
using System.Collections.Generic;
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
            hastriggered=true;
            Playable.Play();
        }
    }
}
