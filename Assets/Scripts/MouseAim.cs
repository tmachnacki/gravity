using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public GameObject crosshairs;
    Vector3 target;

    Camera main_camera;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = main_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
        crosshairs.transform.position = new Vector2(target.x, target.y);
    }
}
