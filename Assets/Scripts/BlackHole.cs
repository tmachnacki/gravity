using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float speed;
    public float move_duration;
    public float life_span;
    Rigidbody rigidbody;
    MouseAim mouse;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.Find("Player").GetComponent<MouseAim>();
        target = mouse.crosshairs.transform.position;

        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(MoveBlackHole());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveBlackHole()
    {
        
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

       

        GameObject.Find("PullTrigger").GetComponent<CircleCollider2D>().enabled = true;
        GameObject.Find("Core").GetComponent<CircleCollider2D>().enabled = true;
        /*
        Collider2D[] objectColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        for (int i = 0; i < objectColliders.Length; ++i)
        {
            // todo: respawn player / reload level
            if (!objectColliders[i].CompareTag("Player") && objectColliders[i].gameObject.GetComponent<SingularityPullable>())
            {
                Destroy(objectColliders[i].gameObject);
            }
        }   
        */

    }
}
