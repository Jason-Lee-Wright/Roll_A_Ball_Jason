using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Waterkill : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody body;

    Vector3 Spawn;

    private void Start()
    {
        Spawn = Player.transform.position;
    }

    private void OnTriggerEnter()
    {
        Player.transform.position = Spawn;
        body.velocity = Vector3.zero;
    }
}
