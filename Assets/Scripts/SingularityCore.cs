using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityCore : MonoBehaviour
{
    float delay = 0.75f;

    public bool destroy = false;
    bool activated = false;

    public float gravity_duration = 30.0f;
    
     void OnTriggerStay2D (Collider2D other) {
        if(other.GetComponent<SingularityPullable>()){
            if (other.CompareTag("Player") && !activated)
            {
                activated = true;
                if (PlayerState.is_inverted)
                {
                    StartCoroutine(other.GetComponent<PlayerMovement>().ChangeGravityGradually(gravity_duration, 18.2f));
                }
                else
                {
                    StartCoroutine(other.GetComponent<PlayerMovement>().ChangeGravityGradually(-gravity_duration, -18.2f));
                }
                // other.GetComponent<PlayerMovement>().InvertGravity();
                // StartCoroutine(other.GetComponent<PlayerMovement>().ColorChange());
                
            }
            else if (other.CompareTag("Rock"))
            {
                //other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(delay);
        if(GetComponent<CircleCollider2D>()){
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    public void DestroyObjects()
    {
        destroy = true;
    }

    
    
}
