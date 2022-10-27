using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    Camera main_cam;
    // Start is called before the first frame update
    void Start()
    {
        main_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = main_cam.transform.position;
    }
}
