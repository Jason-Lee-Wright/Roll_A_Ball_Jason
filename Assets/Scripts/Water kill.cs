using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Waterkill : PlayerController
{
    public GameObject Player;

    private void OnTriggerEnter()
    {
        Player.transform.position = Spawn;
    }
}
