using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Debug.LogError("[FollowTarget.LateUpdate] is missing Target");
            return;
        }

        Vector3 destination_position = new Vector3(target.position.x, 0.0f, 0.0f) + offset;
        //transform.position = Vector3.Lerp(transform.position, destination_position, 0.1f);
        transform.position = destination_position;

    }
}
