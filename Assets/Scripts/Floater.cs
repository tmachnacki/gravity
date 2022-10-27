using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public bool is_horizontal;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public bool is_enabled = true;


 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    public void Restart()
    {
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        if (is_enabled)
        {
            // Float up/down with a Sin()
            tempPos = posOffset;
            if (is_horizontal)
            {
                tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
            }
            else
            {
                tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
            }
 
            transform.position = tempPos;
        }
    }


}
