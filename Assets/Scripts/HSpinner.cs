using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

    
public class HSpinner : MonoBehaviour
{
    public int speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }
}
